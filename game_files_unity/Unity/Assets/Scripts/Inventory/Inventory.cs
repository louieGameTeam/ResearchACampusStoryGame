using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory {

    // List of all Item types in the game
    public static List<Item> globalItems {
        get {
            if (globalItemsPrivate == null)
                Initialize();
            return globalItemsPrivate;
        }
    }

    // Dictionary storing quick references to all items by their name
    public static Dictionary<string, Item> itemsByName {
        get {
            if (itemsByNamePrivate == null)
                Initialize();
            return itemsByNamePrivate;
        }
    }

    // List of all currently owned items and how many are owned
    public static Dictionary<Item, int> backpack {
        get {
            if (backpackPrivate == null) {
                if (globalItems == null)
                    Initialize();
                SaveData sd = SaveData.ReadGameSave();
                if (sd == null)
                    backpackPrivate = new Dictionary<Item, int>();
                else
                    backpackPrivate = sd.inventory;
            }
            return backpackPrivate;
        }
    }

    // Private variables that store cached values
    private static List<Item> globalItemsPrivate;
    private static Dictionary<string, Item> itemsByNamePrivate;
    private static Dictionary<Item, int> backpackPrivate;


    // Initialize and populate lists of all items in the game
    static void Initialize () {

        globalItemsPrivate = new List<Item>();
        itemsByNamePrivate = new Dictionary<string, Item>();

		ItemDB idb = ScriptableObject.CreateInstance<ItemDB>();
		idb = (ItemDB)Resources.Load<ItemDB>("ItemDB");
        foreach (Item item in idb.items) {
            globalItemsPrivate.Add(item);
            itemsByNamePrivate.Add(item.name, item);
        }
	}

    // Add the the given amount of items with the given name to the user's inventory
    public static void Add (string name, int count) {
        if (count <= 0) {
            if (count < 0)
                Remove(name, -count);
            return;
        }
        Item target;
        if (!itemsByName.TryGetValue(name, out target))
            Debug.LogError("Unknown item name: " + name);
        if (backpack.ContainsKey(target)) {
            backpack[target] += count;
        } else {
            backpack.Add(target, count);
        }
        GameObject.FindObjectOfType<InventoryUI>().Glow();

        if (Tasks.currentLevel.currentTask.currentStep is ItemStep) Tasks.Evaluate();

        SaveData.WriteGameSave();
    }

    // Add 1 item with the given name to the user's inventory
    public static void Add (string name) {
        Add(name, 1);
    }

    // Remove the the given amount of items with the given name from the user's inventory
    public static void Remove (string name, int count) {
        if (count <= 0) {
            if (count < 0)
                Add(name, -count);
            return;
        }
        Item target;
        if (!itemsByName.TryGetValue(name, out target))
            Debug.LogError("Unknown item name: " + name);
        if (backpack.ContainsKey(target)) {
            backpack[target] -= count;
            if (backpack[target] <= 0)
                backpack.Remove(target);
            GameObject.FindObjectOfType<InventoryUI>().Glow();
        }

        if (Tasks.currentLevel.currentTask.currentStep is ItemStep) Tasks.Evaluate ();

        SaveData.WriteGameSave();
    }

    // Remove 1 item with the given name from the user's inventory
    public static void Remove (string name) {
        Remove(name, 1);
    }

    // Return the integer value representing the count of the given item in the user's inventory
    public static int Count (string name) {
        Item target;
		if (!itemsByName.TryGetValue (name, out target))
			return 0;
        int count = 0;
        backpack.TryGetValue(target, out count);
        return count;
    }
}



// Class containing all relevent information for an item
[System.Serializable]
public class Item {

    public string name = string.Empty;
    public string description = string.Empty;
    public Sprite icon;

    public Item () {}

}
