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
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Resume()
    {
        GameController.instance.ResumeGame();
        gameObject.SetActive(false);
    }
    
    public void Exit()
    {
        GameController.instance.GameFinished();
        gameObject.SetActive(false);
    }
}
