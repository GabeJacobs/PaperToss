using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCheat : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheatCollider"))
        {
            EventManager.TriggerEvent("IsCheating");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CheatCollider"))
        {
            EventManager.TriggerEvent("IsDoneCheating");
        }
    }
}
