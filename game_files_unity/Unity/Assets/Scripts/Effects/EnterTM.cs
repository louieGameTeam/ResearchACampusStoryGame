using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTM : MonoBehaviour
{

    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Tasks.currentLevel.currentTask.name == "EnterTM")
        {
            player.transform.position = new Vector3(-185.0f, -35.5f, player.transform.position.z);
        }
    }
}
