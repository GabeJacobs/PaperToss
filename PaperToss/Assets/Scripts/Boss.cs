using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MovingCharacter
{

    public AudioClip getBackToWorkClip;
    
    public void PlayAngryVoice()
    {
        Debug.Log("GET BACK TO WORK!");
    }
    
    public void FinishedAngryPoint()
    {
        animator.SetBool("TurnLeft", false);
        StartCoroutine(RotateUp(Vector3.up * 90, 0.6f));
    }
    
}
