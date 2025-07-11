using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Controller {

	// The chattable object that the player is closest to (in case multiple chattable objects are in range)
	[HideInInspector] public Chattable closestInteractable;

	// List of all interactable chattable objects that are in range
	[HideInInspector] public List<Chattable> allInteractable = new List<Chattable>();

    public float runSpeed;

	public static bool teleportBlocked { get; private set; }



	public void Start () {

        SaveData sd = SaveData.ReadGameSave();
        if (sd != null)
            transform.position = sd.playerPos;

        Tasks.UpdateProgress();
	}



	void Update () {

		// Process and respond to movement input every frame
		if (ChatManager.chatting == null)
			Walk(Movement());

		// Press the Submit button to start talking to the nearest available NPC if not currently chatting
		if (Input.GetButtonDown ("Submit")) {
			if (closestInteractable) {
				if (ChatManager.chatting == null) {
					ChatManager.StartChatting (closestInteractable.dialog);
                    GetComponent<Animator>().SetFloat(Controller.speedHash, 0);
                    rb.velocity = Vector2.zero;
				}
			}
		}

        //if (Input.GetKeyDown(KeyCode.T)) // For debugging
            //Tasks.Advance();
	}

	// Transform the player's rigidbody based on directional input
	Vector2 Movement () {

        // If paused, set the vector to 0
        if (MenuControl.inMenu) {
            rb.velocity = Vector2.zero;
            return Vector2.zero;
        }

		// Get raw user input (WASD)
		float v = Input.GetAxis ("Vertical");
		float h = Input.GetAxis ("Horizontal");

		// Generate a directional vector based on inputs
		Vector3 dir = Vector3.zero;
		if (v < 0)
			dir += Vector3.down;
		else if (v > 0)
			dir += Vector3.up;
		if (h < 0)
			dir += Vector3.left;
		else if (h > 0)
			dir += Vector3.right;

		// Normalize vector so moving diagonally isn't faster, and scale with speed multiplier
        float usedSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            usedSpeed = runSpeed;
		dir = dir.normalized * usedSpeed;

		// Set the player's Rigidbody2D velocity to the directional vector
		rb.velocity = dir;

		// Return the directional vector for use in other functions (eg: Walk())
		return dir;
	}
        

	void OnTriggerStay2D(Collider2D other) {

        // This advances Task progress for location based tasks
        if (Tasks.currentLevel == null) return;
		Step cs = Tasks.currentLevel.currentTask.currentStep;
		if (cs is LocationStep && other == ((LocationStep)cs).other)
			Tasks.Advance();
	}

    private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("TeleportBlock"))
			teleportBlocked = true;
    }

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.CompareTag("TeleportBlock"))
			teleportBlocked = false;
	}
}
