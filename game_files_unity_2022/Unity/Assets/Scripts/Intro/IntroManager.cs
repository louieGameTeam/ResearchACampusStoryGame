using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour {
    // TODO: Add error checking

    public GameObject introductionPage;
    public GameObject levelTitle;
    public GameObject controls;
    public GameObject introButton;
    public GameObject introImage;
    public GameObject IntroGhost;

    bool wasTriggered;

	void Start () {
        if (Time.realtimeSinceStartup > 5f)
        {
            Destroy(IntroGhost);
            Destroy(introductionPage);
        }
        StartCoroutine(LevelDisplay());
        Button btn = introButton.GetComponent<Button>();
        btn.onClick.AddListener(HideIntro);
        introButton.SetActive(false);
        controls.SetActive(false);
        wasTriggered = false;
    }

	void Update () {
        if (Input.GetButtonDown("Submit") && !wasTriggered)
            StartCoroutine(SlowShow()); 
    }

    IEnumerator SlowShow()
    {
        wasTriggered = true;
        yield return new WaitForSecondsRealtime(3);
        introImage.SetActive(false);
        introButton.SetActive(true);
    }

    IEnumerator LevelDisplay()
    {
        yield return new WaitForSecondsRealtime(3);
        levelTitle.SetActive(false);
    }

    IEnumerator ControlDisplay()
    {
        yield return new WaitForSecondsRealtime(6);
        Destroy(IntroGhost);
        Destroy(introductionPage);
        
    }


    void HideIntro()
    {
        introButton.SetActive(false);
        controls.SetActive(true);
        StartCoroutine(ControlDisplay());
        //Destroy(IntroGhost);
        //Destroy(introductionPage);
        //IntroGhost.SetActive(false);
        //introductionPage.SetActive(false);
    }
}
