using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Chattable))]
public class ChattableEditor : Editor {
	public override void OnInspectorGUI() {

		Chattable myTarget = (Chattable)target;
        myTarget.dialogData = EditorGUILayout.ObjectField("Dialogue", (Object)myTarget.dialogData, typeof(TextAsset), false) as TextAsset;
        myTarget.requireLineOfSight = EditorGUILayout.Toggle("Require line of sight", myTarget.requireLineOfSight);
        bool isNPC = !(myTarget.GetComponent<CharacterCreation.Character>() == null);
        if (!isNPC) {
            myTarget.myName = EditorGUILayout.TextField("Name", myTarget.myName);
            myTarget.chatHead = EditorGUILayout.ObjectField("Object Icon", (Object)myTarget.chatHead, typeof(Sprite), false) as Sprite;
        } else {
            myTarget.followPlayer = EditorGUILayout.Toggle("Follow player", myTarget.followPlayer);
        }
	}
}
#endif


public class Chattable : MonoBehaviour {

	// XML document containing this NPC's dialog information
	public TextAsset dialogData;

	// Should this NPC turn to follow the player?
	public bool followPlayer = true;

	// The sprite displayed on the side of the chat banner when interacting with this NPC
	public Sprite chatHead;

    // The name of this object as displayed in the chat banner
    public string myName;

	// The dialog object, constructed with the dialogData TextAsset
	public Dialog dialog;

    // True if line of sight is required for player to initiate dialogue
    public bool requireLineOfSight = true;

	// Reference to the PlayerControl singleton instance
	PlayerControl player;

	// Reference to the child SpriteRenderer containing this player's chat bubble
	SpriteRenderer bubble;

    // If attached to a character, this is the direction it was facing before becoming interactable
    int previousDirection = 0;

    [HideInInspector] public bool isNPC;
	private Controller controller;
    bool following = false;

    // Potential NPC-specific variable properties that can be used to dynamically show dialog
    public Dictionary<string, string> properties {
        get {
            if (propertiesPrivate == null) {
                SaveData sd = SaveData.ReadGameSave();
                string hash = GetInstanceHash();
                if (sd != null && sd.dialogMemory != null && sd.dialogMemory.ContainsKey(hash))
                    propertiesPrivate = sd.dialogMemory[hash].dictionary;
                else
                    propertiesPrivate = new Dictionary<string, string>();
            }
            return propertiesPrivate;
        }
        set {
            propertiesPrivate = value;
        }
    }
    private Dictionary<string, string> propertiesPrivate;


    // Generate unique hash code factoring host's place in hierarchy
    public string GetInstanceHash () {
        Transform t = transform;
        string path = string.Empty;
        while (true) {
            string toAdd = t.GetSiblingIndex().ToString();

            // This is necessary because a bug in Unity causes GetSiblingIndex to always return 0 for root transforms in Standalone builds
            // https://issuetracker.unity3d.com/issues/getsiblingindex-always-returns-zero-in-standalone-builds
            if (t.parent == null) {
                GameObject[] allRoot = SceneManager.GetActiveScene().GetRootGameObjects();
                for (int i = 0; i < allRoot.Length; i++)
                    if (t.gameObject == allRoot[i])
                        toAdd = i.ToString();
            }

            path = "/" + toAdd + path;
            if (t.parent != null)
                t = t.parent;
            else
                break;
        }
        path = SceneManager.GetActiveScene().name + ":" + path;
        return path;
    }



	void Awake() {

        controller = GetComponent<Controller>();
        isNPC = !(GetComponent<CharacterCreation.Character>() == null);

		// Set a reference to the player's GameObject
		player = GameObject.FindObjectsOfType<PlayerControl>()[0];

        // Set a reference to the SpriteRenderer object attached to this NPC
        if (transform.Find("Bubble")) {
            bubble = transform.Find("Bubble").GetComponent<SpriteRenderer>();
            bubble.sortingLayerName = "Foreground";
            bubble.sortingOrder = 100;
        }

		// Prevent NPC from being seen as interactable by the player if there's no given Dialogue information
		if (dialogData == null) {
			Collider2D[] allTriggers = GetComponents<Collider2D>();
			foreach (Collider2D region in allTriggers) {
				if (region.isTrigger)
					region.enabled = false;
			}

		// Otherwise, initialize the dialogue object
		} else {
			ChangeDialog(dialogData);
		}

	}

    void Update() {

        // If follow mode is enabled, animate the NPC to be facing toward the player
        if (isNPC && following && followPlayer) {
			Vector2 dir = player.transform.position - transform.position;
			int dirIndex = controller.GetDirection(dir);
			controller.Walk(dirIndex);
		}
    }

    public void ChangeDialog(TextAsset newDialog) {
		dialogData = newDialog;
		dialog = new Dialog(newDialog, this);
	}

	void OnTriggerEnter2D(Collider2D other) {

		// Runs the rest of the function only when the player enters the trigger specified on this NPC in the inspector
		if (other.tag != "Player")
			return;

        // Enable follow mode so the NPC will update itself to look at the player
        if (isNPC) {
            if (GetLineOfSight(player.transform.position))
                following = true;
            previousDirection = controller.anim.GetInteger(Controller.directionHash);
        }

		// Update closestInteractable on the player
		player.allInteractable.Add(this);
	}

	void OnTriggerExit2D(Collider2D other) {

		if (other.tag != "Player")
			return;

		// Disable follow mode and make the NPC face down
		following = false;
        if (isNPC)
            controller.Walk(previousDirection);

		// Update closestInteractable on the player
		player.allInteractable.Remove(this);
		UpdateNearest();
	}

	void OnTriggerStay2D(Collider2D other) {

		if (other.tag != "Player")
			return;

		// Continue updating every frame the player moves within the trigger region
		UpdateNearest();
	}

    // Return true if line-of-sight conditions are met for dialogue to be possible
    public bool GetLineOfSight (Vector3 playerPos) {
        if (!requireLineOfSight)
            return true;
            
        RaycastHit2D[] allHits = Physics2D.RaycastAll(playerPos, transform.position - playerPos);
        bool hitCollider = false;
        bool willHit = false;
        foreach (RaycastHit2D hit in allHits) {
            if (hitCollider || hit.transform == player.transform)
                continue;
            if (!hit.collider.isTrigger) {
                if (transform != hit.transform) {
                    hitCollider = true;
                    continue;
                }
                else
                    willHit = true;
            }
        }
        if (!willHit) {
            if (following && isNPC) {
                controller.Walk(previousDirection);
            }
            following = false;
        }
        return willHit;
    }

	// Update the closestInteractable variable on the player object
	void UpdateNearest() {


		// Set local NPC variable closestNPC to be the closest interactable NPC to the player
		Chattable closestNPC = null;
		float shortestDistance = Mathf.Infinity;
		foreach (Chattable item in player.allInteractable) {
			Vector3 playerPos = player.transform.position;
			float dist = Vector3.Distance(item.transform.position, playerPos);
            if (!item.GetLineOfSight(playerPos))
                continue;
            item.following = true;
			if (dist < shortestDistance) {
				closestNPC = item;
				shortestDistance = dist;
			}
		}

		if (closestNPC != player.closestInteractable) {

			// Toggle the previous closest NPC off
			if (player.closestInteractable != null)
				player.closestInteractable.ToggleClosestInteractable(false);

			// Set PlayerControl.closestInteractable to closestNPC, even if null, and toggle it on
			player.closestInteractable = closestNPC;
			if (closestNPC != null)
				closestNPC.ToggleClosestInteractable(true);
		}
	}

	// Activate or deactivate this NPC as the closest interactable NPC
	public void ToggleClosestInteractable(bool active) {
		bubble.enabled = active;
	}
}