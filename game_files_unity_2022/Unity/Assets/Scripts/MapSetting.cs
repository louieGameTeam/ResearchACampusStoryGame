using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSetting : MonoBehaviour {

    public GameObject teleport;
    public Map[] maps;

    private Map current;
    private Transform player;
    private Image icon;
    RectTransform yah;
    RectTransform teleports;

	void Start () {
        icon = transform.Find("Icon").GetComponent<Image>();
        yah = icon.transform.Find("YAH").GetComponent<RectTransform>();
        teleports = icon.transform.Find("Teleports").GetComponent<RectTransform>();

        player = GameObject.FindWithTag("Player").transform;
        if (maps.Length > 0)
            SetMap(maps[0]);
        else
            this.enabled = false;
	}

    public void UpdateMap() {
        // Update current map
        if (!current.Contains(player.position)) {
            foreach (Map item in maps) {
                if (item.Contains(player.position))
                    SetMap(item);
            }
        }

        UpdateYAH();
    }

    // Update "You Are Here"
    private void UpdateYAH () {
        Vector3 norm = current.Normalize(player.position);
        norm = Translate(norm, current.map.rect.size, icon.rectTransform.rect.size);
        yah.anchorMin = norm;
        yah.anchorMax = norm;
        yah.gameObject.SetActive(current.Contains(player.position));
    }

    // Adjust player position on map to match onto the sprite
    Vector2 Translate(Vector2 pos, Vector2 spriteSize, Vector2 rectSize) {
        float mapAspect = (float)spriteSize.x / (float)spriteSize.y;
        float rectAspect = (float)rectSize.x / (float)rectSize.y;
        if (rectAspect > mapAspect) {
            float mapScaledWidth = mapAspect * rectSize.y;
            float leftOffset = (rectSize.x - mapScaledWidth) / 2f;
            pos.x *= mapScaledWidth / rectSize.x;
            pos += Vector2.right * (leftOffset / rectSize.x);
        } else {
            float mapScaledHeight = rectSize.x / mapAspect;
            float bottomOffset = (rectSize.y - mapScaledHeight) / 2f;
            pos.y *= mapScaledHeight / rectSize.y;
            pos += Vector2.up * (bottomOffset / rectSize.y);
        }
        return pos;
    }

    void SetMap (Map input) {
        current = input;
        icon.sprite = input.map;

        // Delete old quick travel labels
        for (int i = teleports.childCount - 1; i >= 0; i--)
            DestroyImmediate(teleports.GetChild(i).gameObject);

        // Instantiate new quick travel links
        foreach (Map.Teleport item in current.teleports) {
            GameObject label = Instantiate(teleport) as GameObject;
            label.transform.SetParent(teleports);
            Vector2 coord = item.visualCoord == Vector2.zero ? item.coordinate : item.visualCoord;
            Vector2 posNorm = current.Normalize(coord);
            Vector2 norm = Translate(posNorm, current.map.rect.size, icon.rectTransform.rect.size);
            RectTransform rt = label.GetComponent<RectTransform>();
            rt.anchorMin = norm;
            rt.anchorMax = norm;
            rt.localScale = Vector3.one;
            Text text = label.GetComponent<Text>();
            text.resizeTextForBestFit = false;
            text.text = item.name;
            Vector2 size = new Vector2(text.preferredWidth, text.preferredHeight);
            text.resizeTextForBestFit = true;
            size += 4 * Vector2.one;
            rt.offsetMin = -size / 2f;
            rt.offsetMax = size / 2f;
            Vector2 destination = item.coordinate;
            Rect rect = current.region;
            label.GetComponent<Button>().onClick.AddListener(() => {
                if (PlayerControl.teleportBlocked) return;
                //BlackFade.StartFade(false, 0.5f, () => {
                    player.position = destination;
                    UpdateYAH();
                //});
            });
        }
    }


    [System.Serializable]
    public class Map {

        public Sprite map;
        public Vector2 min;
        public Vector2 max;
        public Teleport[] teleports;
        
        public Rect region {
            get {
                return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
            }
        }

        public Vector2 Normalize (Vector2 input) {
            float xNorm = (input.x - min.x) / region.size.x;
            float yNorm = (input.y - min.y) / region.size.y;
            return new Vector2(xNorm, yNorm);
        }

        public bool Contains (Vector2 pos) {
            return region.Contains(pos);
        }

        [System.Serializable]
        public class Teleport {
            public string name = "";
            public Vector2 coordinate = Vector2.zero;
            public Vector2 visualCoord = Vector2.zero;
        }
    }
}
