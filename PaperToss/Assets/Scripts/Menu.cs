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
        ArcadeGameController.instance.StartArcadeCountdown();
    }

    void ToggleShowMenu()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

}
