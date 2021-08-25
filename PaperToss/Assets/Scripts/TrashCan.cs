using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class TrashCan : MonoBehaviour {
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other) {
        if (ArcadeGameController.instance.arcadeIsRunning)
        {
            if (other.tag == "Ball")
            {
                ArcadeGameController.instance.PlayerDidScore();
                other.gameObject.GetComponent<Ball>().isInBasket = true;
            }      
            else if (other.tag == "GoldBall")
            {
                ArcadeGameController.instance.PlayerDidScoreGoldBall();
                other.gameObject.GetComponent<Ball>().isInBasket = true;
            }     
        }
    }
}