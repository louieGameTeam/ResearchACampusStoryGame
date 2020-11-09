using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade: MonoBehaviour
{
    public bool fadeIn = true;
    Image image;
    Color c;

    // speed for how fast/slow the fade
    public float fadeSpeed = 1f;

    void Start()
    {
        image = GetComponent<Image>();
        c = image.color;
   
        // set the alpha of the color
        if (fadeIn)
            c.a = 1;
        else
            c.a = 0;
    }

    void Update()
    {
        // change the alpha over time
        if (fadeIn)
            c.a -= Time.deltaTime / fadeSpeed;
        else
            c.a += Time.deltaTime / fadeSpeed;

        image.color = c;
    }
}
