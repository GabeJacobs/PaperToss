using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTCharacterController : MonoBehaviour
{
    public static PTCharacterController instance { get; private set; }
    public MovingCharacter BossCharacter;

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
        BossCharacter.StopAndDoLeftTurn();;
    }

    // Start is called before the first frame update
    void Start()
    {
        startBossWalk();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startBossWalk()
    {
        BossCharacter.animator.SetBool("Walking", true);
    }

   
}
