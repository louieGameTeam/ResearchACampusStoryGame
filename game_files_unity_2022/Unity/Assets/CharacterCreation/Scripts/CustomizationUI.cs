using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CharacterCreation {
    
    public class CustomizationUI : MonoBehaviour {

        static int direction = 0;

        public List<TabView> tabs;
        public TabView currentTab;
        public CharacterSetting cs;

		List<TabView> BuildTabs() {

            Wardrobe w = ScriptableObject.CreateInstance<Wardrobe>();
            w = Wardrobe.Singleton;
			List<TabView> t = new List<TabView>();

			// Build 'Hair' tab
			TabView hair = new TabView();
            FeatureView hairHair = new FeatureView(w.locations[1], cs.locations[1]);
			hair.features.Add(hairHair);
			t.Add(hair);

			// Build 'Body' Tab
			TabView body = new TabView();
			FeatureView skin = new FeatureView(w.locations[0], cs.locations[0]);
			FeatureView eyes = new FeatureView(w.locations[2], cs.locations[2]);
			body.features.Add(skin);
			body.features.Add(eyes);
			t.Add(body);

			// Build 'Clothes' tab
			TabView clothes = new TabView();
			FeatureView top = new FeatureView(w.locations[3], cs.locations[3]);
            FeatureView pants = new FeatureView(w.locations[4], cs.locations[4]);
			FeatureView shoes = new FeatureView(w.locations[5], cs.locations[5]);
			clothes.features.Add(top);
			clothes.features.Add(pants);
			clothes.features.Add(shoes);
			t.Add(clothes);

			return t;
		}

        void Start() {
            cs = FindObjectOfType<Character>().cs;
            tabs = BuildTabs();
            TabClick(0);

            GameObject p = GameObject.Find("Player");
            GameObject center = GameObject.Find("CharacterBackground/Center");
            p.transform.position = center.transform.position;

            transform.Find("NameInputField").GetComponent<InputField>().text = cs.name;

            FindObjectOfType<Animator>().SetFloat("Speed", 1);
        }

        public void TabClick (int index) {
			currentTab = tabs[index];
			currentTab.Load();
            int i = 0;
            foreach (TabView item in tabs) {
                Transform tab = transform.Find("Tabs").GetChild(i);
                Image img = tab.Find("Image").GetComponent<Image>();
                img.enabled = currentTab == item;
                i++;
            }
        }

        public void FeatureClick (int index) {
            currentTab.currentFeature = currentTab.features[index];
            currentTab.currentFeature.Load();
        }

		public void SaveAll() {
            string toScene = MainMenu.activeScene;

            // Return to Main Menu if this is the first load
            SaveData sd = SaveData.ReadGameSave();
            if (sd == null || (sd != null && sd.character == null)) {
                toScene = "MainMenu";
                MainMenu.toSelect = true;
            }

            SaveData.WriteCharacterSave();
            //#if UNITY_EDITOR
            //if (string.IsNullOrEmpty(toScene)) {
            //    toScene = "MainMenu";
            //    Debug.LogWarning("Character Customization scene should not be entered directly from editor - this will cause errors."); 
            //}
            //#endif
            BlackFade.LoadScene(toScene);
            //FindObjectOfType<SwitchScene>().ChangeScene(toScene);
		}

        public void UpdateName (string input) {
            cs.name = input;
        }

        public void SelectionArrow (bool up) {
            currentTab.currentFeature.Change(up);
        }

        public void TurnArrow (bool left) {
            direction += left ? 1 : -1;
            if (direction > 3)
                direction = 0;
            else if (direction < 0)
                direction = 3;
            Controller c = FindObjectsOfType<Controller>()[0];
            c.Walk(direction);
		}

		public void Randomize() {
            int tabIndex = tabs.IndexOf(currentTab);
            int featureIndex = currentTab.features.IndexOf(currentTab.currentFeature);
            Character c = FindObjectOfType<Character>();
            cs = new CharacterSetting(c.transform);
            c.cs = cs;
            tabs = BuildTabs();
            TabClick(tabIndex);
            FeatureClick(featureIndex);
		}
    }


    public class TabView {
        public List<FeatureView> features;
        public FeatureView currentFeature;

        public TabView () {
            features = new List<FeatureView>();
        }

        public void Load() {
            Transform picker = GameObject.FindObjectOfType<CharacterCreation.CustomizationUI>().transform.Find("Picker");
            Transform featureRow = picker.Find("Features");
            if (features.Count > 0) {
                featureRow.gameObject.SetActive(true);
	            for (int i = 0; i < 4; i++) {
	                Transform f = featureRow.GetChild(i);
	                if (i < features.Count) {
	                    f.gameObject.SetActive(true);
	                    f.Find("Text").GetComponent<Text>().text = features[i].loc.name;
	                } else {
	                    f.gameObject.SetActive(false);
	                }
	            }
            } else {
                featureRow.gameObject.SetActive(false);
            }
            if (currentFeature == null)
                currentFeature = features[0];
            currentFeature.Load();
        }
    }


	public class FeatureView {
        public Location loc;
        int currentOption = 0;
        FeatureSetting fs;

        public FeatureView(Location _loc, FeatureSetting _fs) {
            loc = _loc;
            fs = _fs;
        }

        public void Load () {
			Transform picker = GameObject.FindObjectOfType<CharacterCreation.CustomizationUI>().transform.Find("Picker");
			RectTransform p = picker.Find("Palette").GetComponent<RectTransform>();
			if (loc.versions.Count > 1) {
				picker.Find("Selection").gameObject.SetActive(true);
                p.anchorMax = new Vector2(1, 0.725f);
            } else {
                picker.Find("Selection").gameObject.SetActive(false);
				p.anchorMax = new Vector2(1, 0.85f);
            }
			for (int i = 0; i < loc.versions.Count; i++) {
				if (loc.versions[i].name == fs.version.name)
					currentOption = i;
			}
            for (int i = 0; i < picker.Find("Features").childCount; i++) {
                Image img = picker.Find("Features").GetChild(i).GetComponent<Image>();
                Color selected = new Color32(45, 125, 255, 255);
                float darker = 0.6f;
                Color notSelected = new Color(selected.r * darker, selected.g * darker, selected.b * darker, 1);
                if (img.transform.Find("Text").GetComponent<Text>().text == loc.name)
                    img.color = selected;
                else
                    img.color = notSelected;
                UpdateSelection();
            }

			for (int i = p.childCount - 1; i >= 0; i--)
				Object.DestroyImmediate(p.GetChild(i).gameObject);
            for (int i = 0; i < loc.locationPalette.colors.Count; i++) {
                GameObject item = GameObject.Instantiate(p.parent.Find("Template").gameObject) as GameObject;
                item.SetActive(true);
                Color c = loc.locationPalette.colors[i];
                GameObject colorLayer = item.transform.Find("Color").gameObject;
                Image selectionLayer = item.transform.Find("Selection").GetComponent<Image>();
                item.transform.SetParent(p);
                item.GetComponent<RectTransform>().localScale = Vector3.one;
				colorLayer.GetComponent<Image>().color = c;
				colorLayer.GetComponent<Button>().onClick.AddListener(() => {
                    Color selection = c;
                    fs.tint = selection;
                    for (int j = 0; j < p.childCount; j++)
                        p.GetChild(j).Find("Selection").GetComponent<Image>().enabled = false;
                    selectionLayer.enabled = true;
                });
                selectionLayer.enabled = fs.tint == c;
            }

			Rect rect = p.GetComponent<RectTransform>().rect;
			GridLayoutGroup glp = p.GetComponent<GridLayoutGroup>();
            glp.spacing = Vector2.one * 20;
			int columns = glp.constraintCount;
			float largestX = (rect.width - glp.spacing.x * (columns - 1)) / columns;
			int rows = Mathf.CeilToInt(loc.locationPalette.colors.Count / columns);
			float largestY = (rect.height - glp.spacing.x * (rows - 1)) / rows;
			float min = Mathf.Min(largestX, largestY);
			glp.cellSize = Vector2.one * min;
			if (largestY == min) {
				float x = (rect.width - (float)columns * min) / (float)(columns - 1);
				glp.spacing = new Vector2(x, glp.spacing.y);
			}

        }

        // Change the currently selected sprite version
        public void Change(bool up) {
            currentOption += up ? 1 : -1;
            if (currentOption < 0)
                currentOption = loc.versions.Count - 1;
            else if (currentOption >= loc.versions.Count)
                currentOption = 0;
            UpdateSelection();
		}

        // Update the currently selected version of the currently viewed feature
        private void UpdateSelection () {
            if (loc.versions.Count == 0) return;
			Transform picker = GameObject.FindObjectOfType<CustomizationUI>().transform.Find("Picker");
			Text t = picker.Find("Selection/Text").GetComponent<Text>();
			t.text = loc.versions[currentOption].name;
            fs.version = loc.versions[currentOption];
		}
    }

}