using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class TrashCan : MonoBehaviour {
    // Start is called before the first frame update

    public static event Action onScoreEvent;
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Ball")
        {
            EventManager.TriggerEvent("PlayerScored");
            SoundManager.PlaySound("point");
            other.gameObject.GetComponent<Ball>().isInBasket = true;
             
            if (onScoreEvent != null)
            {
                onScoreEvent.Invoke();
            }
        }
    }
}