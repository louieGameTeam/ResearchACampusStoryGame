using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTown : MonoBehaviour
{

    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update(){
        if (Tasks.currentLevel.currentTask.name == "HelloMarket1" || Tasks.currentLevel.currentTask.name == "HelloMarket2"){
            player.transform.position = new Vector3(41.0f, -40.0f, player.transform.position.z);

        } else if (Tasks.currentLevel.currentTask.name == "GoodbyeALPHA"){
            player.transform.position = new Vector3(174.0f, 108.0f, player.transform.position.z);

        } else if (Tasks.currentLevel.currentTask.name == "HelloALPHA") {
            player.transform.position = new Vector3(-6.0f, 108.0f, player.transform.position.z);
        }
    }
}
