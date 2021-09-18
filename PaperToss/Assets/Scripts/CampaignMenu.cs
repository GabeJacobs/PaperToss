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
    public GameObject mainMenu;
    public GameObject stageOneUI;
    public GameObject stageTwoUI;
    public GameObject prevPageButton;
    public GameObject nextPageButton;
    private int currentPageNumber;


    private void Start()
    {
        currentPageNumber = 1;
        EventManager.TriggerEvent("CheckLevelUnlocks");
        gameObject.SetActive(false);
    }

    public void ShowMainMenu()
    {
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void GoToNextPage()
    {
        currentPageNumber++;
        if (currentPageNumber == 2)
        {
            stageOneUI.SetActive(false);
            stageTwoUI.SetActive(true);
            prevPageButton.SetActive(true);
            nextPageButton.SetActive(false);

        }
    }
    
    public void GoToPrevPage()
    {
        currentPageNumber--;
        if (currentPageNumber == 1)
        {
            stageOneUI.SetActive(true);
            stageTwoUI.SetActive(false);
            prevPageButton.SetActive(false);
            nextPageButton.SetActive(true);
        }
    }
}
