using System.Collections;
using System.Collections.Generic;
using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class LeaderboardMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject rowPrefab;
    public Transform rowsParent;
    public GameObject prevButton;
    public GameObject nextButton;
    public GameObject loadingUI;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowMainMenu()
    {
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
        ResetData();
    }

    public void UpdateUIWithLeaderboard(LeaderboardEntryList leaderboard)
    {
        loadingUI.SetActive(false);
        ResetData();

        foreach (var entry in leaderboard)
        {
            GameObject newRow = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newRow.GetComponentsInChildren<Text>();
            texts[0].text = entry.Rank.ToString();
            texts[1].text = entry.User.DisplayName.ToString();
            texts[2].text = entry.Score.ToString();
        }
        
        UpdatePageButtons();
    }

    private void ResetData()
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
    }

    public void UpdatePageButtons()
    {
        nextButton.SetActive(OculusLeaderboardManager.instance.leaderboard.HasNextPage);
        prevButton.SetActive(OculusLeaderboardManager.instance.leaderboard.HasPreviousPage);
    }

    public void PrevButtonPressed()
    {
        OculusLeaderboardManager.instance.GetPrevPage();
    }

    public void NextButtonPressed()
    {
        OculusLeaderboardManager.instance.GetNextPage();
    }

    public void setLoading(bool isLoading)
    {
        loadingUI.SetActive(isLoading);
    }
    
    
}
