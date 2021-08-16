using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;
 
public class GameController : MonoBehaviour {
 
    public static GameController instance { get; private set; }
    public GameObject holster;
    public GameObject menu;
    public GameCountdown gameCountdown;
    public TimerCountdown timerCountdown;
    public GameObject highScoreUI;

    // Use this for initialization
    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);  
        }
        DontDestroyOnLoad (gameObject);
    }

    public void StartArcadeCountdown()
    {
        menu.SetActive(false);
        holster.SetActive(true);
        gameCountdown.StartCountdown();
    }
    public void StartArcade()
    {
        timerCountdown.BeginCountdown();
    }
    
    public void ArcadeFinished()
    {
       menu.SetActive(true);
    }
    public void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }
    public void ToggleHolster()
    {
        holster.SetActive(!holster.activeSelf);
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