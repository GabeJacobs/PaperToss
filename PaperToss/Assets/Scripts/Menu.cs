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

    private void OnEnable()
    {
        EventManager.StartListening("ToggleShowMenu", ToggleShowMenu);

    }

    private void OnDisable()
    {
        EventManager.StopListening("ToggleShowMenu", ToggleShowMenu);
    }
    
    public void StartArcade()
    {
       EventManager.TriggerEvent("ToggleShowMenu");
       EventManager.TriggerEvent("StartCountdown");

       // gameObject.SetActive(false);
    }

    void ToggleShowMenu()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

}
