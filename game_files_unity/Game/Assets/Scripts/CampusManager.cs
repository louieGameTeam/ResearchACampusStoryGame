using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampusManager : MonoBehaviour {
    //// XML document containing the dialogue information 
    //[SerializeField] TextAsset interactionOne; /* needs better name*/
    //[SerializeField] TextAsset interactionTwo; /* needs better name*/
    //[SerializeField] TextAsset interactionThree; /* needs better name*/

    //// Used to set the dialogue of the various NPCs
    //public Dialog dialogueNPC;
    //public Dialog dialoguePC;
    //public Dialog dialogueProf;
    //public Dialog dialogueAdv;

    //// NPC component on the friend NPC in the scene
    //NPC friendNPC;
    //NPC pcNPC;

    //// Controller component on the player in the scene
    //Controller player;

    //// Speed of player--used to freeze the player
    //float playerSpeed;

    //// Sprite to change chatHead of dialogue
    //public Sprite npcHead;
    //public Sprite pcHead;
    //public Sprite profHead;
    //public Sprite advisorHead;
    
    //// Distance between player and friendNPC
    //float dist;

    ///// Boolean controls
    //// Signal to know if the player is close to friendNPC
    //bool isNear = false;
    //// A reset signal used to make sure the update function doesn't repeatedly start a chat
    //bool oneCall = true;
    //// Signal used to determine if the friendNPC should initiate a chat with the player
    //bool shouldChat = false;

    //bool libraryChat = false;


    ///* TODO
    // * add turn on/turn off event functionality?
    // */

    //void Start() {
    //    // Initilize the components to their respective gameObjects
    //    friendNPC = GameObject.FindGameObjectWithTag("NPC").GetComponent<NPC>();
    //    pcNPC = GameObject.FindGameObjectWithTag("PC").GetComponent<NPC>();
    //    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();

    //    // Get player's speed from player's controller
    //    playerSpeed = player.speed;
    //}

    //void Update() {
    //    // Updates dist between player and friendNPC
    //    dist = Vector2.Distance(player.transform.position, friendNPC.transform.position);

    //    // Evaluates if the player is close enough to friendNPC
    //    if (dist <= 4)
    //    {
    //        isNear = true;
    //        if (oneCall && shouldChat)
    //        {
    //            // Triggers chat with friendNPC
    //            startChat(dialogueNPC);
    //            // Keeps from repeatedly initializing chat
    //            oneCall = false;
    //        }     
    //    }
    //    else
    //        isNear = false;

    //    // Stops friendNPC from spontaneously starting chat when player is near
    //    if (ChatManager.chatting == null && isNear)
    //    {
    //        // Allows player to move freely
    //        freezePlayer(false);
    //        // Stops friendNPC from following player
    //        activateMove(false);
    //    }
    //}

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(other.tag == "SceneStart")
    //    {
    //        // Set friendNPC dialogue for this interaction
    //        dialogueNPC = new Dialog(interactionOne);
    //        friendNPC.dialog = dialogueNPC;

    //        // Set chatHead to friendNPC's head
    //        dialogueNPC.chatHead = npcHead;
    //    }

    //    if (other.tag == "Dorm")
    //    {
    //        dialoguePC = new Dialog(interactionThree);
    //        pcNPC.dialog = dialoguePC;

    //        dialoguePC.chatHead = pcHead;

    //    }

    //    if (other.tag == "Library")
    //    {
    //        // Set friendNPC dialogue for this interaction
    //        dialogueNPC = new Dialog(interactionTwo);
    //        friendNPC.dialog = dialogueNPC;

    //        // Set chatHead to friendNPC's head
    //        dialogueNPC.chatHead = npcHead;

    //        /*Delete: for testing only*/
    //        //Debug.Log("Hello!");
    //    }
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.tag == "SceneStart")
    //    {
    //        // Allows for triggered to be called again RENAME
    //        oneCall = true;
    //        // Enables chatting
    //        shouldChat = true;

    //        // Freezes player and moves friendNPC to player's location
    //        freezePlayer(true);
    //        activateMove(true);

    //        // Destroys the collider so that this interaction won't happen again
    //        Destroy(other);
    //    }

    //    if (other.tag == "Dorm")
    //    {
    //        libraryChat = true;
    //        Destroy(other);

    //        //add some sort of checking to make sure the student sent an email
    //        //sending the email will trigger a cutscene which will be of day -> night -> morning
    //    }

    //    if (other.tag == "Library")
    //    {
    //        if(libraryChat == true)
    //        {
    //            // Allows for triggered to be called again RENAME
    //            oneCall = true;
    //            // Enables chatting
    //            shouldChat = true;

    //            // Freezes player and moves friendNPC to player's location
    //            freezePlayer(true);
    //            activateMove(true);
    //        }

    //        /*Delete: for testing only*/
    //        //Debug.Log("Library!");
    //    }
    //}
        
    //void startChat(Dialog dialog)
    //{
    //    // Faces player in the direction of friendNPC
    //    Vector2 dir = friendNPC.transform.position - player.transform.position;
    //    player.Walk(dir);

    //    // Calls the StartChatting function in the ChatManager script on the chat dialogue component
    //    ChatManager.StartChatting(dialog);
    
    //    // Chat should not proceed again unless reset elsewhere
    //    shouldChat = false;
    //}

    //// Calls the moveToPlayer function in the NPC script on friendNPC
    //void activateMove(bool signal)
    //{
    //    if (signal)
    //        friendNPC.moveToPlayer = true;
    //    else
    //        friendNPC.moveToPlayer = false;
    //}

    //// Freeze the player by adjusting the player's speed
    //void freezePlayer(bool signal)
    //{
    //    if (signal)
    //        player.speed = 0;
    //    else
    //        // playerSpeed is the original player's speed--used to reactivate player movement
    //        player.speed = playerSpeed;
    //}

    //// Pause function coroutine
    //IEnumerator Wait(int waitTime)
    //{
    //    yield return new WaitForSeconds(waitTime);

    //    /*Delete: for testing only*/
    //    //Debug.Log("Heller");
    //}
}