using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

public class CampaignMenu : MonoBehaviour
{
    public Text menuHeader;
    public GameObject arcadeButton;
    public GameObject settingButton;
    public GameObject campaignButton;
    public GameObject leftHandedButton;
    public GameObject rightHandedButton;
    public GameObject mainMenu;


    private void Start()
    { 
        gameObject.SetActive(false);
    }

    public void ShowMainMenu()
    {
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
