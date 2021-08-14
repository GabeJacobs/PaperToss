using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCountdown : MonoBehaviour
{
    int clock = 3;
    public AudioSource audioSource;
    public AudioClip[] clips; // The array controlling the sounds

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
    }

    void StartCountdown()
    {
        audioSource.clip = clips[0];
        audioSource.Play();
        gameObject.SetActive(true);
        Text gameClockTxt = gameObject.GetComponentInChildren<Text>();
        gameClockTxt.text = clock.ToString();
        StartCoroutine(time());
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
        Text gameClockTxt = gameObject.GetComponentInChildren<Text>();
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
        gameObject.SetActive(false);
        EventManager.TriggerEvent("StartArcade");
        //do something
    }
    
    IEnumerator PlayBeginGameClip(){
        audioSource.Play ();
        yield return new WaitWhile (()=> audioSource.isPlaying);
        //do something
    }
}


