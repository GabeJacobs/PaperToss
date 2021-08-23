using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool isInBasket = false;
    public bool isInHolster;

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
}
