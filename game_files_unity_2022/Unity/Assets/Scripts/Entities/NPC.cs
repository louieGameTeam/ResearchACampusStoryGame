using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterCreation.Character))]
public class NPC : Controller {



	// Reference to the PlayerControl singleton instance
	PlayerControl player;

    // Toggle move to player on/off
    [HideInInspector] public bool moveToPlayer = false;

    [SerializeField] Controller.CharacterDirection defaultDirection;



    protected override void Awake () {

        base.Awake();

        direction = (int)defaultDirection;
        Walk(direction);

		// Set a reference to the player's GameObject
		player = GameObject.FindObjectsOfType<PlayerControl>()[0];	
	}
	
	void Update () {

        // If moveToPlayer is enabled, the NPC will move to the player.transform.position
        if (moveToPlayer) {
            Vector2 dir = player.transform.position - transform.position;
            // Keeps the NPC from running into the player, subsiquently pushing him
            //if (!following && !bubble.enabled) {
                speed = 1;
                transform.Translate(dir * speed * Time.deltaTime);
            //}
            Walk(dir);
            speed = 0;
        }
	}
}