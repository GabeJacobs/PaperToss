using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : MovingCharacter
{


    private void Start()
    {
        
    }

    public void FinishedAngryPoint()
    {
        animator.SetBool("TurnLeft", false);
        RightTurn();
    }
    
}
