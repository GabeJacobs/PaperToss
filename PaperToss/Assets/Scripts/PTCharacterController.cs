using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTCharacterController : MonoBehaviour
{
    public static PTCharacterController instance { get; private set; }
    public Animator bossAnimator;
    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);  
        }
        DontDestroyOnLoad (gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered trigger");
        bossAnimator.SetBool("ShouldWalk", false);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startBossWalk()
    {

        bossAnimator.SetBool("ShouldWalk", true);
    }
}
