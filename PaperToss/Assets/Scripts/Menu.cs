using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

public class Menu : MonoBehaviour
{
    public Text menuHeader;
    public GameObject arcadeButton;
    public GameObject settingButton;
    public GameObject leaderboardButton;
    public GameObject campaignButton;
    public GameObject leftHandedButton;
    public GameObject rightHandedButton;
    public GameObject settingsUI;
    public GameObject campaignMenu;
    public GameObject leaderboardMenu;


    private void Start()
    {
        settingsUI.SetActive(false);
        SetHandenessButtonStyles();
    }

    public void StartArcade()
    {
        GameController.instance.SetUpGame(GameMode.Arcade);
        GameController.instance.StartGameCountdown();
    }
    
    public void ShowCampaign()
    {
        gameObject.SetActive(false);
        campaignMenu.SetActive(true);
    }


    void ToggleShowMenu()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
    
    public void ShowMainMenu()
    {
        arcadeButton.SetActive(true);
        settingButton.SetActive(true);
        leaderboardButton.SetActive(true);
        campaignButton.SetActive(true);
        settingsUI.SetActive(false);
        menuHeader.text = "Paper Toss VR";
    }
    
    public void ShowSettings()
    {
        settingsUI.SetActive(true);
        arcadeButton.SetActive(false);
        settingButton.SetActive(false);
        leaderboardButton.SetActive(false);
        campaignButton.SetActive(false);
        menuHeader.text = "Settings";
    }
    
    public void ShowLeaderboard()
    {
        gameObject.SetActive(false);
        leaderboardMenu.SetActive(true);
        OculusLeaderboardManager.instance.RefreshLeaderboard();
    }

    public void SetHandedness(bool isLeft)
    {
        PlayerPrefs.SetInt("isLefty", isLeft ? 1 : 0);
        SetHandenessButtonStyles();
    }

    void SetHandenessButtonStyles()
    {
        bool isLeft = Convert.ToBoolean(PlayerPrefs.GetInt("isLefty"));
        Color darkRedColor = new Color32(146,0, 0, 255);

        if (isLeft)
        {
        
            leftHandedButton.GetComponent<ProceduralImage>().color = darkRedColor;
            leftHandedButton.GetComponent<ProceduralImage>().BorderWidth = 0;

            rightHandedButton.GetComponent<ProceduralImage>().color = Color.white;
            rightHandedButton.GetComponent<ProceduralImage>().BorderWidth = 2;
        }
        else
        {
            rightHandedButton.GetComponent<ProceduralImage>().color = darkRedColor;
            rightHandedButton.GetComponent<ProceduralImage>().BorderWidth = 0;
            
            leftHandedButton.GetComponent<ProceduralImage>().color = Color.white;
            leftHandedButton.GetComponent<ProceduralImage>().BorderWidth = 2;
        }

    }
}
