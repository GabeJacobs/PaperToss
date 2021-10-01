using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Ball : MonoBehaviour
{
    
    public bool isInBasket = false;
    public bool isInHolster;
    public bool isGoldBall;
    public bool isFireBall;
    public Material goldMaterial;
    public Material normalMaterial;
    public GameObject fire;

    void OnEnable ()
    {
        EventManager.StartListening ("MissOccured", MissOccured);
    }

    void OnDisable ()
    {
        EventManager.StopListening ("MissOccured", MissOccured);
    }
    
    private void Start()
    {
        isInHolster = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInBasket && other.CompareTag("DestroyZone"))
        {
            EventManager.TriggerEvent("MissOccured");
            GameController.instance.MissOccured();
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
    
    public void SetFireBall(bool fireBall)
    {
        isFireBall = fireBall;
        fire.SetActive(fireBall);
        if (fireBall == true)
        {
            SetGoldBall(false);
        }
    }

    void MissOccured()
    {
        SetFireBall(false);
    }
}
