using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCountdown : MonoBehaviour
{
    int clock = 3;
    public AudioSource audioSource;
    public AudioClip[] clips; // The array controlling the sounds
    private Coroutine timerCoroutine;
    private Text gameClockTxt;

    private void OnEnable()
    {
        
        EventManager.StartListening("StartCountdown", StartCountdown);

    }

    private void OnDisable()
    {
        EventManager.StopListening("StartCountdown", StartCountdown);
    }

 
    public void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        gameClockTxt = gameObject.GetComponentInChildren<Text>();

    }

    public void StartCountdown()
    {
        clock = 3;
        gameClockTxt.enabled = true;

        gameClockTxt.text = clock.ToString();
        audioSource.clip = clips[0];
        audioSource.Play();
        gameClockTxt.text = clock.ToString();
        
        timerCoroutine = StartCoroutine(time());
    }
    IEnumerator time(){
        while (clock != 0)
        {
            yield return new WaitForSeconds(1);
            timeCount();
        }
    }
    // ReSharper disable Unity.PerformanceAnalysis
    void timeCount(){
        clock -= 1;
        if (clock != 0)
        {
            audioSource.clip = clips[0];
            audioSource.Play();
            gameClockTxt.text = clock.ToString();
        }
        else
        {
            StartCoroutine(PlayBeginGameThenGo());
        }

    }
    
    IEnumerator PlayBeginGameThenGo(){
        audioSource.clip = clips[1];
        yield return StartCoroutine(PlayBeginGameClip());
        GameController.instance.StartArcade();
        StopAllCoroutines();
        gameClockTxt.enabled = false;
        //do something
    }
    
    IEnumerator PlayBeginGameClip(){
        audioSource.Play ();
        yield return new WaitWhile (()=> audioSource.isPlaying);
        //do something
    }
}


