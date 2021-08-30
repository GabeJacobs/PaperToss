using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

public class PauseMenu : MonoBehaviour
{
    public Text menuHeader;
    public GameObject exitButton;
    public GameObject resumeButton;
    public InputActionReference toggleRefernce = null;
    public bool isVisible;
    public Vector3 normalPos;

    private void Awake()
    {
        toggleRefernce.action.started += Toggle;
    }

    private void OnDestroy()
    {
        toggleRefernce.action.started -= Toggle;
    }

    private void Start()
    {
        normalPos = transform.position;
        SetVisible(false);
    }
    
    void Toggle(InputAction.CallbackContext context)
    {
        if (ArcadeGameController.instance.arcadeIsRunning == true && ArcadeGameController.instance.arcadeIsPaused)
        {
            Resume();
        }
        else if (ArcadeGameController.instance.arcadeIsRunning == true )
        {
            SetVisible(!isVisible);
            ArcadeGameController.instance.PauseGame();
        }
    }
    
    public void SetVisible(bool visible)
    {
        // Component[] canvases = gameObject.GetComponentsInChildren(typeof(Canvas));
        //
        // foreach (Component r in canvases)
        // {
        //     Canvas c = (Canvas) r;
        //     c.enabled = visible;
        // }

        if (visible)
        {
            transform.position = normalPos;
        }
        else
        {
            transform.position = new Vector3(10033, 0, 0);
        }
        isVisible = visible;

    }

    public void Resume()
    {
        SetVisible(false);
        ArcadeGameController.instance.ResumeGame();

    }
    
    public void Exit()
    {
        SetVisible(false);
        ArcadeGameController.instance.ArcadeFinished();
    }
}
