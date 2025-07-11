using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChattablePC : Chattable {

    [SerializeField] RectTransform ending;

	public void ShowEnding () {
        StartCoroutine(Animation());
	}

    IEnumerator Animation () {

        ChatManager.GetCM().enabled = false;
        FindObjectOfType<PlayerControl>().enabled = false;
        EventSystem.current.enabled = false;
        List<Graphic> graphics = new List<Graphic>();
        foreach (Graphic item in ending.GetComponentsInChildren<Graphic>())
            graphics.Add(item);
        const float fadeTime = 3f;
        for (float i = 0; i < fadeTime; i += Time.deltaTime) {
            float lerp = i / fadeTime;
            foreach (Graphic item in graphics) {
                Color c = item.color;
                item.color = new Color(c.r, c.g, c.b, lerp);
            }
            yield return null;
        }
        yield return new WaitForSeconds(2);
        graphics.RemoveAt(0);
        for (float i = 0; i < fadeTime; i += Time.deltaTime) {
            float lerp = 1f - i / fadeTime;
            foreach (Graphic item in graphics) {
                Color c = item.color;
                item.color = new Color(c.r, c.g, c.b, lerp);
            }
            yield return null;
        }
        Tasks.Advance();
    }

}
