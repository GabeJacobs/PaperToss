using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MovingCharacter
{

    public AudioClip getBackToWorkClip;
    public AudioClip yourFired;

    public void PlayAngryVoice()
    {
        voice.clip = yourFired;
        voice.Play();
    }
    
    public void FinishedAngryPoint()
    {
        animator.SetBool("TurnLeft", false);
        StartCoroutine(RotateUp(Vector3.up * 90, 0.6f));
    }
    
}
