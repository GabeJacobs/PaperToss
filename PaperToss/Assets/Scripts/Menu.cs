using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    private void Awake()
    {
        
    }

    public void StartArcade()
    {
        GameController.instance.StartArcadeCountdown();

       //  EventManager.TriggerEvent("StartArcade");
       //
       //  EventManager.TriggerEvent("ToggleShowMenu");
       // EventManager.TriggerEvent("StartCountdown");

       // gameObject.SetActive(false);
    }

    void ToggleShowMenu()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

}
