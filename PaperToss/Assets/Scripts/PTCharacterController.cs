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
        startBossWalk();
        // startGhostWalk();

    }

    public void startBossWalk()
    {
        boss.animator.SetBool("Walking", true);
    }
    public void startGhostWalk()
    {
        ghost.WalkForward();
    }
   
}
