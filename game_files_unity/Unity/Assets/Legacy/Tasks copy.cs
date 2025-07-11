/*using System.Collections;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;



public static class Tasks  {

	// List of all Task types in the game
    public static List<Level> allLevels {
        get {
			if (allLevelsPrivate == null) {
                SaveData sd = SaveData.ReadGameSave();
                if (sd == null) {
                    LevelData t = ScriptableObject.CreateInstance<LevelData>();
                    t = (LevelData)Resources.Load<LevelData>("LevelXML");
                    allLevelsPrivate = LoadLevels(t.levelData);
                } else {
                    allLevelsPrivate = sd.journal;
                }
			}
			return allLevelsPrivate;
        }
        //set {
        //    allLevelsPrivate = value;
        //}
    }
	static List<Level> allLevelsPrivate;

	// Level object representing the level that the player is currently on
	public static Level currentLevel {
		get {
			foreach (Level level in allLevels)
				if (level.progress < 1.0f)
					return level;
			return allLevels[0];
		}
	}




    // Convert list of level XML files into a list of Level objects
    private static List<Level> LoadLevels (List<TextAsset> input) {

		List<Level> all = new List<Level> ();
        for (int i = 0; i < input.Count; i++) {
			all.Add (new Level (input[i]));
		}
        return all;
	}

    // Manually increment the progress on the current step
    public static void Advance () {
        currentLevel.currentTask.currentStep.Advance();
    }
}

public class ProgressType {

    public bool completed = false;
    public float progress;

    public virtual void OnBegin () {
        
    }

    public virtual void OnComplete () {
        
    }
}

// Class containing name and list of tasks for a level
[System.Serializable]
public class Level {

	public string name = string.Empty;
    public string scene = string.Empty;
	public List<Task> tasks;
    public bool completed = false;
	public Task currentTask {
		get {
			foreach (Task task in tasks)
				if (!task.completed && task.progress < 1.0f)
					return task;
			return tasks[0];
		}
	}
	public float progress {
		get {
			float p = 0;
            foreach (Task task in tasks) {
                float additional = task.progress;
                p += additional;
            }
			p /= tasks.Count;
            if (p >= 1 && !completed) {
                completed = true;
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
			return p;
		}
	}

	public Level(TextAsset item) {
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(item.text);
		XmlNode root = doc.SelectSingleNode("level");
		name = root.Attributes.GetNamedItem("name").Value;
        scene = root.Attributes.GetNamedItem("scene").Value;
		tasks = new List<Task>();
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
                tasks.Add(toAdd);
                if (hasMove)
                    toAdd.destination = destination;
                toAdd.useDestination = hasMove;
                hasMove = false;
            }
        }
	}
}

// Class containing all relevent information for a task
[System.Serializable]
public class Task {

    public string name = string.Empty;
	public string description = string.Empty;
	public bool completed = false;
	[SerializeField] Dictionary<string, int> completion;
	public List<Step> steps;
    public Vector2 destination;
    public bool useDestination = false;
    public Step currentStep {
        get {
            Step c = steps[steps.Count - 1];
            foreach (Step step in steps) {
                step.active = false;
                if (!step.completed && step.progress < 1.0f)
                    c = step;
            }
            c.active = true;
            if (useDestination)
                GameObject.FindObjectOfType<PlayerControl>().transform.position = destination;
            return c;
        }
    }
    public float progress {
        get {
            float p = 0;
            foreach (Step step in steps) {
                if (step.completed)
                    p++;
                else
                    p += step.progress;
            }
            p /= steps.Count;
            if (p >= 1.0f && !completed) {
                completed = true;
                foreach (KeyValuePair<string, int> item in completion)
                    Inventory.Add(item.Key, item.Value);
            }
            return p;
        }
    }

	public Task (XmlNode item) {
		name = item.SelectSingleNode("name").InnerText;
        description = item.SelectSingleNode("description").InnerText;
		steps = new List<Step> ();
		foreach (XmlNode step in item.SelectSingleNode("steps")) {
            switch (step.Name.ToLower()) {
			case "location":
				steps.Add (new LocationStep (step));
				break;
			case "item":
				steps.Add (new ItemStep (step));
				break;
            default:
				steps.Add (new Step (step));
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

// Base class for wall task step types
[System.Serializable]
public class Step {

    public bool active = false;
	public int target = 0;
    [SerializeField] int actualCurrent = 0;
    public int current {
        get {
            return actualCurrent;
        }
        set {
            actualCurrent = value;
            if (value >= target && !completed) {
                completed = true;
                float callGetAccessors;
                Step cs;
                do {
					cs = Tasks.currentLevel.currentTask.currentStep;
					callGetAccessors = Tasks.currentLevel.progress;
                } while (Tasks.currentLevel.currentTask.currentStep.progress >= 1.0f && Tasks.currentLevel.currentTask.currentStep != cs);
            }
            SaveData.WriteGameSave();
        }
    }
    public bool completed = false;

    public float progress {
        get {
            if (active)
			    Evaluate ();
            float p = (float)current / (float)target;
            p = Mathf.Clamp01(p);
			return p;
        }
    }

	public Step (XmlNode input) {
        XmlNode count = input.Attributes.GetNamedItem("count");
        target = count != null ? int.Parse(count.Value) : 1;
	}

    // Update current progress
	public virtual void Evaluate () {}

    // Advance current progress by 1
    public void Advance () {
        current++;
    }

	public int GetIndex () {
		int index = 0;
		for (int i = 0; i < Tasks.allLevels.Count; i++) {
			for (int j = 0; j < Tasks.allLevels[i].tasks.Count; j++) {
				for (int k = 0; k < Tasks.allLevels [i].tasks [j].steps.Count; k++) {
					if (Tasks.allLevels [i].tasks [j].steps [k] == this)
						return index;
					index++;
				}
			}
		}
		return index;
	}
}

// Location based step objective
[System.Serializable]
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
[System.Serializable]
public class ItemStep : Step {

	[SerializeField] string itemName;

    public ItemStep (XmlNode input) : base(input) {
		itemName = input.Attributes.GetNamedItem ("name").Value;
	}

	public override void Evaluate () {
        current = Inventory.Count(itemName);
	}
}*/