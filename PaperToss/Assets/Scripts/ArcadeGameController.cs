using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using Random=UnityEngine.Random;

public class ArcadeGameController : MonoBehaviour {
 
    public static ArcadeGameController instance { get; private set; }

    public bool arcadeIsRunning;
    public float currentFanSpeed = 0;
    public float gameDifficulty = 0;

    public Fan fan;
    public Holster holster;
    public GameObject menu;
    public GameObject highScoreUI;
    public GameCountdown gameStartCountdown;
    public TimerCountdown timerCountdown;
    public ScoreCounter scoreboard;


    // Use this for initialization
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
        fan = GameObject.FindGameObjectWithTag("Fan").GetComponent<Fan>();;
        holster = GameObject.FindGameObjectWithTag("Holster").GetComponent<Holster>();
        menu = GameObject.FindGameObjectWithTag("MainMenu");
        gameStartCountdown = GameObject.FindGameObjectWithTag("GameStartCountdown").GetComponent<GameCountdown>();
        timerCountdown = GameObject.FindGameObjectWithTag("TimerCountdown").GetComponent<TimerCountdown>();
        highScoreUI = GameObject.FindGameObjectWithTag("HighScoreUI");
        scoreboard = GameObject.FindGameObjectWithTag("Scoreboard").GetComponent<ScoreCounter>();

    }

    public void StartArcadeCountdown()
    {
        fan.SetVisible(true);
        menu.SetActive(false);
        holster.SetVisible(true);
        gameStartCountdown.StartCountdown();
    }
    public void StartArcade()
    {
        arcadeIsRunning = true;
        timerCountdown.BeginCountdown();
    }
    
    public void ArcadeFinished()
    { 
        fan.SetVisible(false);
        menu.SetActive(true);
        holster.SetVisible(false);
        fan.SetVisible(false);
        timerCountdown.Reset();
        arcadeIsRunning = false;
        gameDifficulty = 0;
        DestoryAllBalls();
       
    }
    
    public void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }
    
    public void PlayerDidScore()
    {
        scoreboard.PlayerScored();
        bool gotNewHighScore = CheckSaveHighScore(scoreboard.score);
        
        increaseGameDifficulty();
        
        
        fan.ChangeFanPosition();
        fan.SetFanSpeedUI();
    }

    private void increaseGameDifficulty()
    {
        gameDifficulty += 0.3f;
        currentFanSpeed = Random.Range(Mathf.Clamp(gameDifficulty - 1.0f, 0.1f, 10.0f) ,Mathf.Clamp(gameDifficulty + 1.0f, 0.1f, 10.0f));

      
    }
    
    private bool CheckSaveHighScore(int newScore)
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        bool gotNewHighScore = newScore > highScore;

        if (gotNewHighScore)
        {
            PlayerPrefs.SetInt("HighScore", newScore);
            PlayerPrefs.Save();
            EventManager.TriggerEvent("NewHighScore");
        }

        return gotNewHighScore;
    }
    
    void DestoryAllBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        for(var i = 0 ; i < balls.Length ; i ++)
        {
            if (!balls[i].GetComponent<Ball>().isInHolster)
            {
                Destroy(balls[i]);
            }
        }
    }
    

}