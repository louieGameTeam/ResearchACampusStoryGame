using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class IntroTextManager : MonoBehaviour
{
    // seconds to wait
    public float waitSeconds = 5f;

    // XML document containing the dialogue information 
    [SerializeField] TextAsset dialogueData;
    public Dialog dialogue;

    void Start()
    {
        // Construct dialogue with TextAsset
        dialogue = new Dialog(dialogueData);
        
        // Intro delay before dialogue popup
        StartCoroutine(IntroDelay());
    }

    IEnumerator IntroDelay()
    {
        yield return new WaitForSeconds(waitSeconds);
        startChat(dialogue);
    }

    public void startChat(Dialog dialog)
    {
        ChatManager.StartChatting(dialog);
    }
}