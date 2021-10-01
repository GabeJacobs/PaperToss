using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : MovingCharacter
{

    public AudioClip getBackToWorkClip;
    public AudioClip[] audioClips;

    private void Start()
    {
        // clips = new
    }

    public void PlayAngryVoice()
    {
        int r = Random.Range(0, audioClips.Length - 1);
        voice.clip = audioClips[r];
        voice.Play();
    }
    
    public void FinishedAngryPoint()
    {
        animator.SetBool("TurnLeft", false);
        RightTurn();
    }
    
}
