using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SocialPlatforms.Impl;
using Random=UnityEngine.Random;
using RenderSettings = UnityEngine.RenderSettings;

public class ArcadeGameController : MonoBehaviour {
 
    public static ArcadeGameController instance { get; private set; }

    public bool arcadeIsRunning;
    public bool arcadeIsPaused;
    public float gameDifficulty = 0;
    public int gameLength;

    public Fan fan;
    public Holster holster;
    public GameObject menu;
    public GameObject pauseMenu;
    public GameObject highScoreUI;
    public GameObject fireworks;
    public GameCountdown gameStartCountdown;
    public TimerCountdown timerCountdown;
    public ScoreCounter scoreboard;
    public TrashCan trashCan;
    public HighScoreTextAnimator highScoreAnmator;

    public AudioClip pointClip;
    public AudioClip goldPointClip;
    public AudioClip arcadeOverClip;

    private bool shouldCelebrateNewHighScore;
    private Coroutine fadeInOutNightLightCoroutine;

    [SerializeField] private Material nightSkyBox;
    [SerializeField] private Material daySkyBox;
    [SerializeField] private Light nightLight;


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
        // PlayerPrefs.DeleteAll();
        
        fan = GameObject.FindGameObjectWithTag("Fan").GetComponent<Fan>();;
        holster = GameObject.FindGameObjectWithTag("Holster").GetComponent<Holster>();
        menu = GameObject.FindGameObjectWithTag("MainMenu");
        gameStartCountdown = GameObject.FindGameObjectWithTag("GameStartCountdown").GetComponent<GameCountdown>();
        timerCountdown = GameObject.FindGameObjectWithTag("TimerCountdown").GetComponent<TimerCountdown>();
        highScoreUI = GameObject.FindGameObjectWithTag("HighScoreUI");
        scoreboard = GameObject.FindGameObjectWithTag("Scoreboard").GetComponent<ScoreCounter>();
        trashCan = GameObject.FindGameObjectWithTag("TrashCan").GetComponent<TrashCan>();
        fireworks = GameObject.FindGameObjectWithTag("Fireworks");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        nightLight = GameObject.FindGameObjectWithTag("NightLight").GetComponent<Light>();
        fireworks.SetActive(false);
        highScoreAnmator.SetVisible(false);

    }

    public void StartArcadeCountdown()
    {
        Debug.Log(nightLight.intensity);
        if (!Mathf.Approximately(0.0f, nightLight.intensity))
        {
            changeSceneLight(true);
        }
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
        arcadeIsPaused = false;
        arcadeIsRunning = true;
        timerCountdown.BeginCountdown();
    }
    
    public void ArcadeFinished()
    { 
        arcadeIsPaused = false;
        SoundManager.Instance.Play(arcadeOverClip);
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
            EventManager.TriggerEvent("NewHighScore");
            runHighScoreAnimations();
        }
    }
    

    private void runHighScoreAnimations()
    {
        fireworks.SetActive(true);
        shouldCelebrateNewHighScore = false;
        highScoreAnmator.SetVisible(true);
        highScoreAnmator.startAnimation();
        changeSceneLight(false);
    }

    private void changeSceneLight(bool day)
    {
        if (day)
        {
            RenderSettings.skybox = daySkyBox;
            if (fadeInOutNightLightCoroutine != null)
            {
                StopCoroutine(fadeInOutNightLightCoroutine);
            }
            StartCoroutine(fadeInOutNightLight(nightLight, false, 1.5f));
        }
        else
        {
            RenderSettings.skybox = nightSkyBox;
            fadeInOutNightLightCoroutine = StartCoroutine(fadeInOutNightLight(nightLight, true, 1.5f));
        }
    }
    IEnumerator fadeInOutNightLight(Light lightToFade, bool fadeIn, float duration)
    {
        float minLuminosity = 0; // min intensity
        float maxLuminosity = 30; // max intensity

        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;

        if (fadeIn)
        {
            a = minLuminosity;
            b = maxLuminosity;
        } else
        {
            a = maxLuminosity;
            b = minLuminosity;
        }

        float currentIntensity = lightToFade.intensity;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            lightToFade.intensity = Mathf.Lerp(a, b, counter / duration);
            yield return null;
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

    public void PauseGame()
    {
        timerCountdown.PauseCountdown();
        arcadeIsPaused = true;
        
        fan.SetVisible(false);
        holster.SetVisible(false);
        trashCan.SetVisible(false);

    }
    
    public void ResumeGame()
    {
        arcadeIsPaused = false;
        timerCountdown.BeginCountdown();
        
        holster.SetVisible(true);
        fan.SetVisible(true);
        trashCan.SetVisible(true);

    }

}