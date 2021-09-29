using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTCharacterController : MonoBehaviour
{
    public static PTCharacterController instance { get; private set; }
    public MovingCharacter bossCharacter;
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
    }

    public void startBossWalk()
    {
        bossCharacter.animator.SetBool("Walking", true);
    }

   
}
