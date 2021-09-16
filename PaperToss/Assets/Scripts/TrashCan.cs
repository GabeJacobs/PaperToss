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
        if (GameController.instance.gameIsRunning && !GameController.instance.gameIsPaused)
        {
            if (other.tag == "Ball")
            {
                if (!other.gameObject.GetComponent<Ball>().isInBasket)
                {
                    if (other.GetComponent<Ball>().isGoldBall == true)
                    {
                        GameController.instance.PlayerDidScoreGoldBall(); 
                        other.gameObject.GetComponent<Ball>().isInBasket = true;
                     
                    }
                    else
                    {
                        GameController.instance.PlayerDidScore();
                        other.gameObject.GetComponent<Ball>().isInBasket = true;
                    }
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