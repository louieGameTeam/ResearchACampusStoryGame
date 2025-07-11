using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPC))]
public class Wander : MonoBehaviour {

    //public Transform[] nodes;
    public Transform path;
    public float nodeDistance = 1;
    public float secondsWait = 5f;

    int currentNode = 0;
    Vector2 dir;
    List<Transform> nodes;

    bool CR_running = false;
    bool stopWander = false;

    Controller c;

    void Awake () {
        c = GetComponent<Controller>();
    }


    private void Start(){

        Transform[] pathTransform = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransform.Length; i++)
        {
            if (pathTransform[i] != path.transform)
            {
                nodes.Add(pathTransform[i]);
            }
        }

        dir = nodes[0].transform.position - transform.position;
        dir = dir.normalized * c.speed;
        c.Walk(dir);
    }

    void FixedUpdate () {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < nodeDistance) {
            if (currentNode == nodes.Count - 1)
                currentNode = 0;
            else
                currentNode++;
            StartCoroutine(Pause(secondsWait));
        }

        dir = nodes[currentNode].transform.position - transform.position;
        dir = dir.normalized * c.speed;


        if (stopWander) {
            c.Walk(Vector2.zero);
        } else if(!CR_running) {
            c.Walk(dir);
            transform.Translate(dir * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (!other.CompareTag("Player"))
            return;
        stopWander = true;
    }

    void OnTriggerExit2D (Collider2D other) {
        if (!other.CompareTag("Player"))
            return;
        stopWander = false;
    }

    IEnumerator Pause (float seconds) {
        
        CR_running = true;
        c.Walk(Vector2.zero);

        yield return new WaitForSecondsRealtime(seconds);

        dir = nodes[currentNode].transform.position - transform.position;
        dir = dir.normalized * c.speed;
        c.Walk(dir);
        CR_running = false;

    }
}
