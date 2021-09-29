using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTCharacterController : MonoBehaviour
{
    public static PTCharacterController instance { get; private set; }
    public MovingCharacter boss;
    public MovingCharacter ghost;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);  
        }
        DontDestroyOnLoad (gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartBossWalk()
    {
        boss.animator.SetBool("Idle", false);
        boss.animator.SetBool("Walking", true);
    }
    public void StartGhostWalk()
    {
        ghost.WalkForward();
    }

    public void ResetAllCharacters()
    {
        boss.Reset();
        ghost.Reset();
    }
   
}
