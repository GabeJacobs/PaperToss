using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    public Text textDisplay;
    public int secondsLeft = 60;
    public bool takingAway = false;
    public bool shouldCountDown = false;

    // Start is called before the first frame update

    private void Awake()
    {
        textDisplay = gameObject.GetComponentInChildren<Text>();
    }

    void Start()
    {
        textDisplay.text = "1:00";
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
        secondsLeft -= 1;
        if (secondsLeft < 10)
        {
            textDisplay.text = "0:0" + secondsLeft;

        }
        else
        {
            textDisplay.text = "0:" + secondsLeft;
        }
        takingAway = false;
        if (secondsLeft == 0)
        {
            GameController.instance.ArcadeFinished();
            shouldCountDown = false;
        }
    }

    public void BeginCountdown()
    {
        shouldCountDown = true;
    }
    
    
}
