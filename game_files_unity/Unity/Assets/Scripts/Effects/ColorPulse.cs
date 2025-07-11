using UnityEngine;
using System.Collections;

public class ColorPulse : MonoBehaviour
{
    Color color1, color2;
    SpriteRenderer sprite;

    void Start(){
        sprite = GetComponent<SpriteRenderer>();
        color1 = new Color(53, 222, 255, 128);
        color2 = new Color(0, 222, 255, 128);
    }

    void Update(){
        Color color = Color.Lerp(color1, color2, Mathf.PingPong(Time.time, 1));
        sprite.color = color;
    }
}