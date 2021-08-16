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

    // Update is called once per frame
    void Update () {
   
    }
    
    
}