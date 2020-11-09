using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Exit()
    {
        float delay = UIManager.current.GetComponent<Fading>().BeginFade(1);
        StartCoroutine(Quit(delay));
    }

    IEnumerator Quit (float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}
