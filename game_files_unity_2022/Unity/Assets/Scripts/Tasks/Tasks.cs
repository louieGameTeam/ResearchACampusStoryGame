using System.Collections;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;



public static class Tasks  {


    public static Game game {
        get {
            if (privateGame == null) {
                SaveData sd = SaveData.ReadGameSave();
                if (sd == null || (sd != null && string.IsNullOrEmpty(sd.taskProgress))) {
                    privateGame = new Game();
                } else {
                    privateGame = new Game(sd.taskProgress);
                }
            }
            return privateGame;
        }
        set {
            privateGame = value;
        }
    }
    private static Game privateGame;

    public static float progress { get { return game.progress; } }
    public static Level currentLevel { get { return game.currentLevel; } }
    public static List<Level> levels { get { return game.levels; } }


    public static void UpdateProgress () {
        game.UpdateProgress();
        if (game.currentLevel == null)
            game.finished = true;
    }

    // Manually increment the progress on the current step
    public static void Advance() {
        game.currentLevel.currentTask.currentStep.Advance();
    }

    // Evaluate the current step for potential progress
    public static void Evaluate () {
        if (currentLevel != null)
            game.currentLevel.currentTask.currentStep.Evaluate();
    }
}

// Base class for Game, Level, Task, and Step classes
//[System.Serializable]
public class ProgressType {

    public float progress = 0;
    public bool completed { get { return progress >= 1.0f; } }


    protected List<ProgressType> list;
    protected ProgressType current {
        get {
            foreach (ProgressType item in list)
                if (!item.completed)
                    return item;
            return null;
        }
    }
    public virtual float UpdateProgress() {
        if (completed) return progress;
        float p = 0;
        foreach (ProgressType item in list) {
            float additional = item.UpdateProgress();
            p += additional;
        }
        p /= list.Count;
        progress = p;
        if (progress >= 1) {
            progress = 1.0f;
            OnComplete();
        }
        return p;
    }

    public ProgressType () {
        list = new List<ProgressType>();
    }

    public virtual void OnBegin () {}

    public virtual void OnComplete () {}
}

//[System.Serializable]
public class Game : ProgressType {

    public bool finished = false;

    public Level currentLevel { 
        get {
            if (!finished)
                return (Level)current;
            else
                return null;
        }
    }
    public List<Level> levels {
        get {
            List<Level> t = new List<Level>();
            foreach (Level item in list)
                t.Add(item);
            return t;
        }
    }

    public Game () : base() {

        LevelData t = (LevelData)Resources.Load<LevelData>("LevelXML");
        for (int i = 0; i < t.levelData.Count; i++)
            list.Add(new Level(t.levelData[i]));
    }

    public Game (string input) : this () {

        if (input.EndsWith("/")) input = input.Remove(input.Length - 1, 1);

        bool started = true;
        if (!input.Contains("/")) {
            //input = input.Remove(0, 2);
            started = false;
        }

        string lvl = string.Empty, tsk = string.Empty;
        int stp = 0, cur = 0;

        int index = input.IndexOf('/');
        int i = 0;
        lvl = index > 0 ? input.Substring(0, index) : input;
        if (int.TryParse(lvl, out i)) {
            if (levels.Count > i)
                lvl = levels[i].name;
            else {
                finished = true;
                return;
            }
        }
        Level theLevel = null;
        foreach (Level item in levels)
            if (item.name == lvl)
                theLevel = item;
        if (index > 0) {
            input = input.Remove(0, index + 1);
            index = input.IndexOf('/');
            tsk = index > 0 ? input.Substring(0, index) : input;
            if (int.TryParse(tsk, out i)) tsk = theLevel.tasks[i].name;
            if (index > 0) {
                input = input.Remove(0, index + 1);
                index = input.IndexOf('/');
                stp = int.Parse(index > 0 ? input.Substring(0, index) : input);
                if (index > 0) {
                    input = input.Remove(0, index + 1);
                    cur = int.Parse(input);
                }
            }
        } else {
            tsk = theLevel.tasks[0].name;
        }

        //Debug.Log(lvl + '\n' + tsk + '\n' + stp + '\n' + cur);

        bool done = true;
        foreach (Level l in levels) {
            float p = 0;
            foreach (Task t in l.tasks) {
                float p1 = 0;
                for (i = 0; i < t.steps.Count; i++) {
                    float toAdd = 0;
                    if (l.name == lvl && t.name == tsk && i == stp) {
                        t.steps[i].current = cur;
                        toAdd = (float)cur / (float)t.steps[i].target;
                        done = false;
                    } else if (done) {
                        toAdd = 1.0f;
                    }
                    p1 += toAdd;
                    t.steps[i].progress = toAdd;
                }
                t.progress = (float)p1 / (float)t.steps.Count;
                p += t.progress;
            }
            l.progress = p / (float)l.tasks.Count;
        }
        currentLevel.started = started;

    }

    public override float UpdateProgress () {
        
        if (currentLevel == null) return 1f;
        Level lastLevel = currentLevel;
        Task lastTask = currentLevel.currentTask;
        Step lastStep = currentLevel.currentTask.currentStep;

        base.UpdateProgress();

        if (currentLevel == null) return 1f;
        bool firstTime = !currentLevel.started && currentLevel.currentTask.currentStep == currentLevel.tasks[0].steps[0];
        if (currentLevel.currentTask != lastTask || firstTime)
            currentLevel.currentTask.OnBegin();
        

        return progress;
    }

    public override string ToString () {

        if (currentLevel == null) return levels.Count.ToString();

        string lvl = currentLevel.name;
        Task ct = currentLevel.currentTask;
        string tsk = ct.name;
        int stpIndex = ct.steps.IndexOf(ct.currentStep);
        string stp = stpIndex.ToString();
        string cur = ct.steps[stpIndex].current.ToString();

        string prog = lvl + "/" + tsk + "/" + stp + "/" + cur;

        //if (currentLevel.started)
        //prog = "S:" + prog;
        if (!currentLevel.started)
            prog = levels.IndexOf(currentLevel).ToString();

        return prog;
    }
}

// Class containing name and list of tasks for a level
//[System.Serializable]
public class Level : ProgressType {

	public string name = string.Empty;
    public string scene = string.Empty;
    public Task currentTask { get { return (Task)current; } }
    public List<Task> tasks {
        get {
            List<Task> t = new List<Task>();
            foreach (Task item in list)
                t.Add(item);
            return t;
        }
    }

    public bool started = false;


    public override void OnComplete () {
        MainMenu.toSelect = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        //BlackFade.LoadScene("MainMenu");
    }

    public Level(TextAsset item) : base() {
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(item.text);
		XmlNode root = doc.SelectSingleNode("level");
		name = root.Attributes.GetNamedItem("name").Value;
        scene = root.Attributes.GetNamedItem("scene").Value;
        Vector2 destination = Vector2.zero;
        bool hasMove = false;
        foreach (XmlNode task in root.ChildNodes) {
            if (task.Name == "move") {
                hasMove = true;
                destination = new Vector2(
                    float.Parse(task.Attributes.GetNamedItem("x").Value),
                    float.Parse(task.Attributes.GetNamedItem("y").Value)
                );
            } else if (task.Name == "task") {
                Task toAdd = new Task(task);
                if (hasMove) {
                    toAdd.destination = destination;
                    toAdd.useDestination = true;
                }
                hasMove = false;
                list.Add(toAdd);
            }
        }
	}
}

// Class containing all relevent information for a task
//[System.Serializable]
public class Task : ProgressType {

    public string name = string.Empty;
	public string description = string.Empty;
	[SerializeField] Dictionary<string, int> completion;
    public Step currentStep { get { return (Step)current; } }
    public Vector2 destination;
    public bool useDestination = false;
    public List<Step> steps {
        get {
            List<Step> t = new List<Step>();
            foreach (Step item in list)
                t.Add(item);
            return t;
        }
    }

    public override void OnBegin () {
        if (useDestination) {
            //if (GameObject.FindObjectOfType<FadeManager>() == null) {
            //    BlackFade.StartFade(false, 1f, () => {
            //        GameObject.FindObjectOfType<PlayerControl>().transform.position = destination;
            //        Tasks.currentLevel.started = true;
            //    });
            //} else {
                GameObject.FindObjectOfType<PlayerControl>().transform.position = destination;
                Tasks.currentLevel.started = true;
            //}
        } else {
            Tasks.currentLevel.started = true;
        }

    }

    public override void OnComplete() {
        base.OnComplete();
        foreach (KeyValuePair<string, int> item in completion)
            Inventory.Add(item.Key, item.Value);
        GameObject.FindObjectOfType<TaskUI>().Glow();
    }

    public Task (XmlNode item) : base() {
		name = item.SelectSingleNode("name").InnerText;
        description = item.SelectSingleNode("description").InnerText;
		foreach (XmlNode step in item.SelectSingleNode("steps")) {
            switch (step.Name.ToLower()) {
			case "location":
				list.Add (new LocationStep (step));
				break;
			case "item":
				list.Add (new ItemStep (step));
				break;
            default:
				list.Add (new Step (step));
				break;
			}
		}
        completion = new Dictionary<string, int>();
        XmlNode comp = item.SelectSingleNode("completion");
        if (comp != null) {
            foreach (XmlNode i in comp) {
                string itemName = i.Attributes.GetNamedItem("name").Value;
                XmlNode count = i.Attributes.GetNamedItem("count");
                int itemCount = count != null ? int.Parse(count.Value) : 1;
                if (i.Name == "take")
                    itemCount *= -1;
                else if (i.Name != "give")
                    Debug.LogError("Error: Invalid node type.");
                completion.Add(itemName, itemCount);
            }
        }
	}
}

// Base class for all task step types
//[System.Serializable]
public class Step : ProgressType {

    public int target { get; private set; }
    public new int current = 0;

    protected void SetCurrent(int input) {
        if (input == current) return;
        float p;
        do {
            p = Tasks.progress;
            current = input;
            Tasks.UpdateProgress();
            Tasks.Evaluate();
        } while (Tasks.progress > p);
        SaveData.WriteGameSave();
    }

    public override float UpdateProgress () {
        float p = (float)current / (float)target;
        p = Mathf.Clamp01(p);
        progress = p;
        if (progress >= 1)
            OnComplete();
		return p;
    }

	public Step (XmlNode input) {
        XmlNode count = input.Attributes.GetNamedItem("count");
        target = count != null ? int.Parse(count.Value) : 1;
	}

    // Update current progress
	public virtual void Evaluate () {}

    // Advance current progress by 1
    public void Advance () {
        SetCurrent(current + 1);
    }

	public int GetIndex () {
		int index = 0;
		for (int i = 0; i < Tasks.levels.Count; i++) {
			for (int j = 0; j < Tasks.levels[i].tasks.Count; j++) {
				for (int k = 0; k < Tasks.levels [i].tasks [j].steps.Count; k++) {
					if (Tasks.levels [i].tasks [j].steps [k] == this)
						return index;
					index++;
				}
			}
		}
		return index;
	}
}

// Location based step objective
//[System.Serializable]
public class LocationStep : Step {

    public Collider2D other {
		get {
            if (targetPrivate == null) {
				targetPrivate = GameObject.Find(inputID).GetComponent<Collider2D>();
            }
            return targetPrivate;
        }
    }
    [System.NonSerialized] Collider2D targetPrivate;
    [SerializeField] string inputID;

    public LocationStep (XmlNode _input) : base(_input) {
        inputID = _input.Attributes.GetNamedItem("name").Value;
    }
}

// Location based step objective
//[System.Serializable]
public class ItemStep : Step {

	[SerializeField] string itemName;

    public ItemStep (XmlNode input) : base(input) {
		itemName = input.Attributes.GetNamedItem ("name").Value;
	}

	public override void Evaluate () {
        SetCurrent (Inventory.Count(itemName));
	}
}