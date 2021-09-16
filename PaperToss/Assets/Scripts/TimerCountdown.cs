using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    public Text textDisplay;
    public bool takingAway = false;
    public bool shouldCountDown = false;
    private int secondsLeft;

    // Start is called before the first frame update

    private void Awake()
    {
        textDisplay = gameObject.GetComponentInChildren<Text>();
    }

    void Start()
    {
        SetClock(GameController.instance.gameLength);
    }

    public void UpdateGameLength()
    {
        SetClock(GameController.instance.gameLength);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldCountDown && !takingAway && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        }
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        SetClock(secondsLeft-1);
        takingAway = false;
        if (secondsLeft == 0)
        {
            GameController.instance.GameFinished();
        }
    }

    public void BeginCountdown()
    {
        shouldCountDown = true;
    }
    public void PauseCountdown()
    {
        shouldCountDown = false;
    }

    public void Reset()
    {
        SetClock(GameController.instance.gameLength);
        takingAway = false;
        shouldCountDown = false;
    }

    private void SetClock(int seconds)
    {
        secondsLeft = seconds;
        if (secondsLeft == 60)
        {
            textDisplay.text = "1:00";
        }
        else if (secondsLeft < 10)
        {
            textDisplay.text = "0:0" + secondsLeft;
        }
        else
        {
            textDisplay.text = "0:" + secondsLeft;
        }
    }
}
