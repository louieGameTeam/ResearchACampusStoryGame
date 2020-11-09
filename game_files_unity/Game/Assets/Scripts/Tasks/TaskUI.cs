using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour {

    [SerializeField] int listBuffer;
    [SerializeField] GameObject template;
    [SerializeField] Transform glow;

    [SerializeField] Sprite check;
    [SerializeField] Sprite empty;



	public void UpdateUI () {
		Transform content = transform.Find("Scroll View/Viewport/Content").transform;
		for (int i = content.childCount - 1; i >= 0; i--)
			DestroyImmediate(content.GetChild(i).gameObject);

		Text title = transform.Find("Title").GetComponent<Text>();
		title.text = "Journal - " + Tasks.currentLevel.name;
		Text percent = transform.Find("Percent").GetComponent<Text>();
		float lvlFloat = Tasks.currentLevel.progress;
		string lvlProgress = Mathf.FloorToInt(lvlFloat * 100).ToString() + "% Complete";
		percent.text = lvlProgress;
		Slider lvl = transform.Find("Level Progress").GetComponent<Slider>();
		lvl.value = lvlFloat;

		int padding = 5;
		for (int i = Tasks.currentLevel.tasks.Count - 1; i >= 0; i--) {
			Task item = Tasks.currentLevel.tasks[i];
			float p = item.progress;
			if (item != Tasks.currentLevel.currentTask && p == 0)
				continue;
			Transform panel = (Instantiate(template) as GameObject).transform;
			panel.gameObject.SetActive(true);
			panel.SetParent(content);
			RectTransform rt = panel.GetComponent<RectTransform>();
			rt.localScale = Vector3.one;

            Sprite pString = check;
            if (p < 1f)
                pString = empty;
			panel.Find("Progress").GetComponent<Image>().sprite = pString;
			panel.Find("Description").GetComponent<Text>().text = item.description;
		}
		int depth = 0;
		for (int i = 0; i < content.childCount; i++) {
			RectTransform panel = content.GetChild(i).GetComponent<RectTransform>();
            panel.offsetMax = Vector2.zero;
            panel.offsetMin = Vector2.zero;
			RectTransform desc = panel.Find("Description").GetComponent<RectTransform>();
            RectTransform prog = panel.Find("Progress").GetComponent<RectTransform>();
			int h = (int)desc.GetComponent<Text>().preferredHeight;
            int height = h + padding * 2;

            desc.offsetMin = Vector2.down * (h + padding + 1);
			desc.offsetMax = Vector2.down * padding;
            int progressHeight = 24;
            prog.offsetMax = Vector2.down * (height - progressHeight) * 0.5f;
            prog.offsetMin = Vector2.down * (height + progressHeight) * 0.5f;
			panel.offsetMax = Vector2.down * depth;
			depth += height;
			panel.offsetMin = Vector2.down * depth;
			depth += listBuffer;
		}
		content.GetComponent<RectTransform>().offsetMin = Vector2.down * depth;
    }

    public void Glow() {
        GameObject.FindObjectOfType<MenuControl>().Glow(glow);
    }

}
