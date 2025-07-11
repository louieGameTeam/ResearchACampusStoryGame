using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterCreation;

[RequireComponent(typeof(Character))]
public class Controller : MonoBehaviour {

	// Walking speed multiplier
	public float speed;

	// References to miscellaneous attached components
    [HideInInspector] public Rigidbody2D rb { get; private set; }
    [HideInInspector] public Animator anim { get; private set; }

	// Declare hashes to efficiently find animator properties
	public static int speedHash = Animator.StringToHash ("Speed");
    public static int directionHash = Animator.StringToHash("Direction");

    protected enum CharacterDirection { Down, Left, Up, Right };
    [HideInInspector] public int direction = 0;

	// The directional vector from the previous frame
	int lastIndex = 0;

    // The attached Character component (required by this class)
    Character character;



	protected virtual void Awake () {

		// Initialize component variables
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
        character = GetComponent<Character>();
	}


    // Update the Animator component's directional properties based on movement vector from Movement()
    public void Walk(Vector2 dir) {

        if (dir.Equals(Vector2.zero)) { // Set the character to the idle position if the input is zero
            character.SetIdle();
        } else if (anim.GetFloat(speedHash) == 0 && dir.magnitude > 0) { // Immediately start walking animation the frame a new direction is specified
            character.Next();
        }

        // Set the animation speed, normalized with the speed multiplier
        float speedNormalized = dir.magnitude / speed;
        anim.SetFloat(speedHash, speedNormalized);

        int dirIndex = 0;
        if (dir.magnitude != 0) {
            dirIndex = GetDirection(dir);
            Walk(dirIndex);
        }

        if (lastIndex != dirIndex) {
            
			// Update lastDir to the new value for the next frame
			lastIndex = dirIndex;

            // Immediately update the directional sprites
            character.Next();
        }
	}

    public int GetDirection (Vector2 dir) {

		// Only attempt to change direction if the player is moving
        int dirIndex = 0;
		if (dir.magnitude != 0) {

            // Calculate angle of the directional vector, 0 to 360, positive CCW, 0 at +x axis
			float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg + 180f;
			float radius = 45f;

			if ((Mathf.Approximately(angle, 45f) && (lastIndex == 2 || lastIndex == 3)) ||
				(Mathf.Approximately(angle, 135) && (lastIndex == 1 || lastIndex == 2)) ||
				(Mathf.Approximately(angle, 225) && (lastIndex == 0 || lastIndex == 1)) ||
				(Mathf.Approximately(angle, 315) && (lastIndex == 3 || lastIndex == 0))) {
				dirIndex = lastIndex;
			}
			else if (angle <= radius || angle >= 360 - radius)
				dirIndex = 3;
			else if (Mathf.Abs(angle - 90f) <= radius)
				dirIndex = 2;
			else if (Mathf.Abs(angle - 180f) <= radius)
				dirIndex = 1;
			else if (Mathf.Abs(angle - 270f) <= radius)
				dirIndex = 0;

            return dirIndex;
        } else {
            return lastIndex;
        }

    }

    public void Walk (int dirIndex) {
		anim.SetInteger(directionHash, dirIndex);
        if (anim.GetFloat(speedHash) == 0)
            character.SetIdle();
        //if (name == "NPC") {
            //print(anim.GetInteger(directionHash) + "  " + Time.time);

        //}
        
    }
}
