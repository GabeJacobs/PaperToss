using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTCharacterController : MonoBehaviour
{
    public static PTCharacterController instance { get; private set; }
    public MovingCharacter boss;
    public MovingCharacter ghost;
    public MovingCharacter sassy;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);  
        }
        DontDestroyOnLoad (gameObject);
    }

    public void StartBossWalk()
    {
        boss.animator.SetBool("Idle", false);
        boss.animator.SetBool("Walking", true);
    }
    
    public void StartSassyWalk()
    {
        sassy.animator.SetBool("Idle", false);
        sassy.animator.SetBool("Walking", true);
    }
    public void StartGhostWalk()
    {
        ghost.WalkForward();
    }

    public void ResetAllCharacters()
    {
        boss.Reset();
        ghost.Reset();
        sassy.Reset();
    }
   
}
