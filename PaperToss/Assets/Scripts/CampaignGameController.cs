using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using Random=UnityEngine.Random;

public class CampaignGameController : MonoBehaviour {
 
    public static CampaignGameController instance { get; private set; }

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
    public AudioClip pointClip;
    public AudioClip goldPointClip;
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
        

    }
    
}