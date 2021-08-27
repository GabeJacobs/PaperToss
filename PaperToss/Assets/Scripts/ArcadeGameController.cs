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
    public GameObject menu;
    public GameObject highScoreUI;
    public GameCountdown gameStartCountdown;
    public TimerCountdown timerCountdown;
    public ScoreCounter scoreboard;
    public TrashCan trashCan;
    public GameObject fireworks;
    public HighScoreTextAnimator highScoreAnmator;

    public AudioClip pointClip;
    public AudioClip goldPointClip;
    public AudioClip arcadeOverClip;

    private bool shouldCelebrateNewHighScore;


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
        trashCan = GameObject.FindGameObjectWithTag("TrashCan").GetComponent<TrashCan>();
        fireworks = GameObject.FindGameObjectWithTag("Fireworks");
        fireworks.SetActive(false);
        highScoreAnmator.SetVisible(false);
    }

    public void StartArcadeCountdown()
    {
        fireworks.SetActive(false);
        highScoreAnmator.SetVisible(false);
        scoreboard.ResetScore();
        fan.SetFanSpeedUI();
        fan.UpdateFanStrength();
        fan.SetVisible(true);
        menu.SetActive(false);
        holster.SetVisible(true);
        holster.SetHolsterPosition();
        gameStartCountdown.StartCountdown();
        trashCan.SetVisible(true);
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
        fan.SetVisible(false);
        trashCan.SetVisible(false);
        
        timerCountdown.Reset();
        arcadeIsRunning = false;
        gameDifficulty = 0;
        fan.currentFanSpeed = 0;
        fan.UpdateFanStrength();
        DestoryAllBalls();

        if (shouldCelebrateNewHighScore == true)
        {
            fireworks.SetActive(true);
            shouldCelebrateNewHighScore = false;
            highScoreAnmator.SetVisible(true);
            highScoreAnmator.startAnimation();
            EventManager.TriggerEvent("NewHighScore");
        }
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
        SoundManager.Instance.Play(goldPointClip);
        scoreboard.PlayerScoredGold();
        updateAfterScore();
    }

    void updateAfterScore()
    {
        bool gotNewHighScore = CheckSaveHighScore(scoreboard.score);
        
        increaseGameDifficulty();
        fan.ChangeFanPosition();
        fan.SetFanSpeedUI();


        if (ShouldShowGoldBall())
        {
            holster.SetNextBallToBeGold();
        }
        else
        {
            holster.SetNextBallToBeNormal();
        }
        
    }

    private bool ShouldShowGoldBall()
    {
        int r = UnityEngine.Random.Range(0, 3);
        if (r == 2)
        {
            return true;
        }
        return false;
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
            shouldCelebrateNewHighScore = true;
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