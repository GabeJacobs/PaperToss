using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    
    public bool isInBasket = false;
    public bool isInHolster;
    public bool isGoldBall;
    public Material goldMaterial;
    public Material normalMaterial;

    private void Start()
    {
        isInHolster = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInBasket && other.CompareTag("DestroyZone"))
        {
            Destroy(gameObject, 2f);
        }
    }

    public void SetGoldBall(bool isGold)
    {
        isGoldBall = isGold;
        MeshRenderer r = gameObject.GetComponentInChildren<MeshRenderer>();
        if (isGold == true)
        {
            r.material = goldMaterial;
        }
        else
        {
            r.material = normalMaterial;
        }
    }
}
