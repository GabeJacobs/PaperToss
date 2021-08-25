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
    public float gameDifficulty = 0;
    public int gameLength;

    public Fan fan;
    public Holster holster;
    public Holster goldHolster;
    public GameObject menu;
    public GameObject highScoreUI;
    public GameCountdown gameStartCountdown;
    public TimerCountdown timerCountdown;
    public ScoreCounter scoreboard;

    public AudioClip pointClip;
    public AudioClip arcadeOverClip;


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
        goldHolster = GameObject.FindGameObjectWithTag("GoldHolster").GetComponent<Holster>();
        menu = GameObject.FindGameObjectWithTag("MainMenu");
        gameStartCountdown = GameObject.FindGameObjectWithTag("GameStartCountdown").GetComponent<GameCountdown>();
        timerCountdown = GameObject.FindGameObjectWithTag("TimerCountdown").GetComponent<TimerCountdown>();
        highScoreUI = GameObject.FindGameObjectWithTag("HighScoreUI");
        scoreboard = GameObject.FindGameObjectWithTag("Scoreboard").GetComponent<ScoreCounter>();

    }

    public void StartArcadeCountdown()
    {
        scoreboard.ResetScore();
        fan.SetFanSpeedUI();
        fan.UpdateFanStrength();
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
        SoundManager.Instance.Play(arcadeOverClip);
        fan.SetVisible(false);
        menu.SetActive(true);
        holster.SetVisible(false);
        goldHolster.SetVisible(false);
        fan.SetVisible(false);
        timerCountdown.Reset();
        arcadeIsRunning = false;
        gameDifficulty = 0;
        fan.currentFanSpeed = 0;
        fan.UpdateFanStrength();
        DestoryAllBalls();
       
    }
    
    public void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }
    
    public void PlayerDidScore()
    {
        SoundManager.Instance.Play(pointClip);
        scoreboard.PlayerScored();
        updateAfterScore();
    }
    
    public void PlayerDidScoreGoldBall()
    {
        SoundManager.Instance.Play(pointClip);
        scoreboard.PlayerScoredGold();
        updateAfterScore();
    }

    void updateAfterScore()
    {
        bool gotNewHighScore = CheckSaveHighScore(scoreboard.score);
        
        increaseGameDifficulty();
        fan.ChangeFanPosition();
        fan.SetFanSpeedUI();

        if (holster.isVisible)
        {
            goldHolster.SetVisible(true);
            holster.SetVisible(false);
        }
        else
        {
            goldHolster.SetVisible(false);
            holster.SetVisible(true);
        }
    }

    private void increaseGameDifficulty()
    {
        gameDifficulty += 0.4f;
        fan.currentFanSpeed = Random.Range(Mathf.Clamp(gameDifficulty - 0.7f, 0.1f, 10.0f), Mathf.Clamp(gameDifficulty + 0.7f, 0.1f, 10.0f));
        fan.UpdateFanStrength();
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

    public float currentFanSpeed()
    {
        return fan.currentFanSpeed;
    }
    

}