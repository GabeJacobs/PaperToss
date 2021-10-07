using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;


public class OculusLeaderboardManager : MonoBehaviour
{
    public static OculusLeaderboardManager instance { get; private set; }
    public LeaderboardEntryList leaderboard;
    private GameObject[] entryObjects;
    private string leaderboardName = "PTLeaderboard";
    public int entriesPerPage = 8;
    public int totalEntries;
    public LeaderboardMenu leadboardMenu;
    
    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);  
        }
        DontDestroyOnLoad (gameObject);
    }
    
    private void Start()
    {
        Oculus.Platform.Core.AsyncInitialize();
        // Users.GetLoggedInUser().OnComplete(msg =>
        // {
        //     if (msg.IsError)
        //     {
        //         Debug.LogError(msg);
        //     }
        //     else
        //     {
        //         LoggedInWithOculusID(msg.GetUser().ID.ToString(), msg.GetUser().OculusID.ToString());
        //     }
        // });
    }

    public void SubmitScore(int score)
    {
        if (score <= 0)
        {
            Debug.Log("Invalid score");
            return;
        }
        Debug.Log("Data saved to leaderboard");
        Leaderboards.WriteEntry(leaderboardName, score);
    }
    
    public void RefreshLeaderboard()
    {
        if (leaderboard != null)
        {
            leaderboard.Clear();
        }
        leadboardMenu.setLoading(true);
        Leaderboards.GetEntries(leaderboardName, entriesPerPage, LeaderboardFilterType.None, LeaderboardStartAt.Top).OnComplete(LeaderboardGetCallback);
    }


    void LeaderboardGetCallback(Message<LeaderboardEntryList> msg)
    {
        if (msg.IsError)
        {
            Debug.LogError("Error getting leaderboard entries");
            return;
        }
        else
        {
            leaderboard = msg.Data;
            totalEntries = Convert.ToInt32(leaderboard.TotalCount);
            leadboardMenu.UpdateUIWithLeaderboard(leaderboard);
        } 
    }

    
    void LoggedInWithOculusID(string oculusID, string oculusName)
    {
        RefreshLeaderboard();
    }


    public void GetNextPage()
    {
        Leaderboards.GetNextEntries(leaderboard).OnComplete(LeaderboardGetCallback);;
    }
    
    public void GetPrevPage()
    {
        Leaderboards.GetPreviousEntries(leaderboard).OnComplete(LeaderboardGetCallback);;

    }
}
