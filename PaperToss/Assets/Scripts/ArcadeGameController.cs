using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;
 
public class ArcadeGameController : MonoBehaviour {
 
    public static ArcadeGameController instance { get; private set; }

    public bool playerCanScore;
    
    public Holster holster;
    public GameObject menu;
    public GameObject highScoreUI;
    public GameCountdown gameStartCountdown;
    public TimerCountdown timerCountdown;
    

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
        holster = GameObject.FindGameObjectWithTag("Holster").GetComponent<Holster>();
        menu = GameObject.FindGameObjectWithTag("MainMenu");
        gameStartCountdown = GameObject.FindGameObjectWithTag("GameStartCountdown").GetComponent<GameCountdown>();
        timerCountdown = GameObject.FindGameObjectWithTag("TimerCountdown").GetComponent<TimerCountdown>();
        highScoreUI = GameObject.FindGameObjectWithTag("HighScoreUI");

    }

    public void StartArcadeCountdown()
    {
        menu.SetActive(false);
        holster.SetVisible(true);
        gameStartCountdown.StartCountdown();
    }
    public void StartArcade()
    {
        playerCanScore = true;
        timerCountdown.BeginCountdown();
    }
    
    public void ArcadeFinished()
    {
       menu.SetActive(true);
       holster.SetVisible(false);
       timerCountdown.Reset();
       
    }
    public void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }

    public void checkHighScore(int newScore)
    {
        SaveHighScore(newScore);
    }

    private bool SaveHighScore(int newScore)
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
    

}