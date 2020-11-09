using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour {

    [SerializeField] RectTransform optionsMenu;
    [SerializeField] RectTransform journalMenu;
    [SerializeField] RectTransform inventoryMenu;
    [SerializeField] RectTransform mapMenu;
    [SerializeField] Text selected;

    [SerializeField] Image background; 

    private static KeyCode optionsKey   = KeyCode.O;
    private static KeyCode journalKey   = KeyCode.J;
    private static KeyCode inventoryKey = KeyCode.I;
    private static KeyCode mapKey       = KeyCode.M;

    private static Vector2 inMin = new Vector2(0.2f, 0.2f);
    private static Vector2 inMax = new Vector2(0.8f, 0.8f);
    private static Vector2 outMin = new Vector2(0.2f, -0.8f);
    private static Vector2 outMax = new Vector2(0.8f, -0.2f);

    private static float animTime = 0.6f;

    private static bool wasTalking = false;
    private static RectTransform currentMenu;
    private static bool animated = false;

    public static bool inMenu { get { return currentMenu != null; } }

	
	void Update () {

        if (!animated) {
            if (Input.GetKeyDown(optionsKey)) {
                if (currentMenu != optionsMenu)
                    optionsMenu.GetComponent<OptionsUpdater>().UpdateCard();
                Show(optionsMenu);
            } else if (Input.GetKeyDown(journalKey)) {
    			journalMenu.GetComponent<TaskUI>().UpdateUI();
    			Show(journalMenu);
    		} else if (Input.GetKeyDown(inventoryKey)) {
                inventoryMenu.GetComponent<InventoryUI>().UpdateInventoryUI();
    			Show(inventoryMenu);
            } else if (Input.GetKeyDown(mapKey)) {
                Show(mapMenu);
                mapMenu.GetComponent<MapSetting>().UpdateMap();
            } else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.R)) {
                if (currentMenu != null) {
                    Hide();
                } else if (ChatManager.chatting == null) {
                    optionsMenu.GetComponent<OptionsUpdater>().UpdateCard();
                    Show(optionsMenu);
                }
            }
        }
	}

    IEnumerator TransitionPanel (RectTransform panel, bool isIn) {

        animated = true;
        background.enabled = true;
        EventSystem current = EventSystem.current;
        if (!current) yield break;
        current.SetSelectedGameObject(null);
        current.enabled = false;
		float t = 0;
        while (t < animTime) {
            LoadTransition(panel, t / animTime, isIn);
            t += Time.deltaTime;
            yield return null;
        }
        LoadTransition(panel, 1, isIn);

        current.enabled = true;
        if (!isIn) {
            background.enabled = false;
            if (wasTalking) GameObject.FindObjectOfType<ChatManager>().enabled = true;
            currentMenu = null;
        }
        animated = false;
    }

    void LoadTransition (RectTransform panel, float t, bool goIn) {

        if (!goIn) t = 1 - t;
        float i = Mathf.Sin(t * Mathf.PI / 2f);

        panel.anchorMin = Vector2.Lerp(outMin, inMin, i);
        panel.anchorMax = Vector2.Lerp(outMax, inMax, i);

        background.color = Color.Lerp(Color.clear, Color.black * 0.67f, t);
    }

    public void OnHover (string title) {
        selected.text = title;
    }

    public void Show (RectTransform panel) {
        if (currentMenu == panel)
            Hide();
        else
            StartCoroutine(HideThenShow(panel));
    }

    IEnumerator HideThenShow (RectTransform panel) {
        if (currentMenu != null) {
			Hide();
			while (currentMenu != null)
			    yield return null;
		}
		currentMenu = panel;
		StartCoroutine(TransitionPanel(panel, true));

		ChatManager cm = GameObject.FindObjectOfType<ChatManager>();
		wasTalking = cm.enabled;
		cm.enabled = false;
    }

	public void Hide () {
        OnHover(string.Empty);
		StartCoroutine (TransitionPanel (currentMenu, false));
	}

    public void Glow(Transform sprite) {
        if (Mathf.Approximately(sprite.localScale.x, 0))
            StartCoroutine(StartGlow(sprite));
    }

    IEnumerator StartGlow(Transform sprite) {

        float maxScale = 6f;
        float durationUp = 2f;
        Vector3 start = Vector3.forward;
        Vector3 end = new Vector3(maxScale, maxScale, 1);

        bool hitMid = false;
        float t = 0;
        while (t >= 0) {
            if (t >= 1)
                hitMid = true;
            t += Time.deltaTime * (hitMid ? -1 : 1) / durationUp;
            float lerp = Mathf.Sin(t * Mathf.PI / 2f);
            sprite.localScale = Vector3.Lerp(start, end, lerp);
            yield return null;
        }
        sprite.localScale = start;
    }
}
