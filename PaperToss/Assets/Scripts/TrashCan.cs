using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class TrashCan : MonoBehaviour {
    private void Start()
    {
        SetVisible(false);
    }
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other) {
        if (ArcadeGameController.instance.arcadeIsRunning && !ArcadeGameController.instance.arcadeIsPaused)
        {
            if (other.tag == "Ball")
            {
                if (other.GetComponent<Ball>().isGoldBall == true)
                {
                    ArcadeGameController.instance.PlayerDidScoreGoldBall(); 
                    other.gameObject.GetComponent<Ball>().isInBasket = true;
                     
                }
                else
                {
                    ArcadeGameController.instance.PlayerDidScore();
                    other.gameObject.GetComponent<Ball>().isInBasket = true;
                }
            }
        }
    }

    public void SetVisible(bool isVisible)
    {
        if (isVisible == false)
        {
            gameObject.GetComponentInChildren<Renderer>().enabled = false;
            gameObject.GetComponentInChildren<Collider>().enabled = false;

        }
        else
        {
            gameObject.GetComponentInChildren<Renderer>().enabled = true;
            gameObject.GetComponentInChildren<Collider>().enabled = true;


        }
    }
}