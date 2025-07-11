using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] RectTransform content;
    [SerializeField] GameObject template;
    [SerializeField] RectTransform glow;
    bool inventorySelected = false;
    int rowHeight;

    // The zero-based index of the currently selected inventory item
    static int selected = -1;

    void Awake() {

        // Set the height of each row in pixels
        rowHeight = Mathf.CeilToInt((float)Screen.height / 30f);
    }

    // Called whenever the UI for the inventory needs to be refreshed
    public void UpdateInventoryUI() {
        
        if (Inventory.backpack.Count == 0) {
            selected = -1;
        } else {
            selected = Mathf.Clamp(selected, 0, Inventory.backpack.Count - 1);
        }

        // Delete existing item listings
        for (int i = content.childCount - 1; i >= 0; i--) {
            DestroyImmediate(content.GetChild(i).gameObject);
        }

        // Generate a row for each item in the player's inventory
        foreach (KeyValuePair<Item, int> pair in Inventory.backpack) {
            GameObject row = Instantiate(template) as GameObject;
            row.transform.SetParent(content);
            row.transform.GetChild(0).GetComponent<Text>().text = pair.Key.name;
            Text count = row.transform.GetChild(1).GetComponent<Text>();
            if (pair.Value > 1)
                count.text = pair.Value.ToString();
            else
                count.text = string.Empty;
            RectTransform rt = row.GetComponent<RectTransform>();
            rt.localScale = Vector3.one;
            rt.offsetMin = new Vector2(8, -content.childCount * rowHeight);
            rt.offsetMax = new Vector2(-12, (-content.childCount + 1) * rowHeight);
            row.SetActive(true);
            row.name = pair.Key.name;
        }
        content.offsetMin = new Vector2(0, -content.childCount * rowHeight);
        content.offsetMax = new Vector2(0, 0);

        UpdateSelected();
    }

    //void Update() {

    //     TODO: This is broken, fix it
    //    // Check if list or return button is selected
    //    if (inventorySelected == true) {
    //        // Process keyboard input to change selection
    //        int direction = (Input.GetButtonDown("Up") ? -1 : 0) + (Input.GetButtonDown("Down") ? 1 : 0);
    //        if (direction != 0) {
    //            selected += direction;
    //            selected = Mathf.Clamp(selected, 0, content.childCount - 1);
    //            UpdateSelected();
    //        }
    //    }
    //}

    // Update UI components to match the newly selected item
    void UpdateSelected() {

        if (selected > content.childCount - 1) selected = 0;

        // Save references to procedurally populated UI elements
        Image icon = transform.Find("Icon").GetComponent<Image>();
        Text desc = transform.Find("Description/Viewport/Content/Text").GetComponent<Text>();
        Text empty = transform.Find("List/Viewport/Empty").GetComponent<Text>();

        // Support for empty inventory
        if (selected < 0) {
            icon.enabled = false;
            desc.text = string.Empty;
            empty.enabled = true;
            return;
        }
        empty.enabled = false;

        // Procedurally populate content based on inventory selection
        for (int i = 0; i < content.childCount; i++) {
            float gray = (i % 2 == 1) ? 0.75f : 0.85f;
            content.GetChild(i).GetComponent<Image>().color = new Color(gray, gray, gray, 1);
        }
        content.GetChild(selected).GetComponent<Image>().color = Color.yellow;
        Item target = Inventory.itemsByName[content.GetChild(selected).name];
        icon.enabled = true;
        icon.sprite = target.icon;
        desc.text = target.description;
    }

    // Update the selected object when the mouse hovers over an item
    public void OnHover(Transform input) {
        selected = input.GetSiblingIndex();
        UpdateSelected();
    }

    public void Glow() {
        GameObject.FindObjectOfType<MenuControl>().Glow(glow);
    }
}