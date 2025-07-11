using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimation : MonoBehaviour {

    public Animation anim;
    void Start()
    {
        anim = GetComponent<Animation>();
        foreach (AnimationState state in anim)
        {
            state.speed = 0.5F;
        }
    }
}