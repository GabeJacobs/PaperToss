using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Fan : MonoBehaviour
{
    public GameObject fanModel;
    public GameObject windSpeedCanvas;
    public GameObject windSpeedText;

    public GameObject[] fanSpawnObjects;
    public int currentFanPositionIndex = 0;
    private Vector3 currentFanPosition;
    public AudioSource fanNoise;
    public FanWind fanWind;
    public float currentFanSpeed = 0;

    public Transform trashBin;

    // Start is called before the first frame update
    void Start()
    {
        fanWind = gameObject.GetComponentInChildren<FanWind>();
        currentFanSpeed = 0;
        placeFanInPosition(currentFanPositionIndex);
        SetVisible(false);
        
    }

    private void placeFanInPosition(int position)
    {


        currentFanPositionIndex = position;
        

        GameObject fawnPositionWaypoint = fanSpawnObjects[currentFanPositionIndex];
        transform.position = fawnPositionWaypoint.transform.position;
        currentFanPosition = transform.position;

        windSpeedText.transform.SetParent(transform.parent);
        transform.LookAt(trashBin.transform.position);
        windSpeedText.transform.SetParent(windSpeedCanvas.transform);
        
        fanWind.direction = (trashBin.transform.position - transform.position).normalized;
        
   
    }

    public void ChangeFanPosition(bool onlyCloseToPlayer)
    {
        if (!onlyCloseToPlayer)
        {
            placeFanInPosition(Random.Range(0,6));
        }
        else
        {
            placeFanInPosition(Random.Range(5,6));
        }
    }
    public void ResetFanPosition()
    {
        placeFanInPosition(0);
    }

    private void SetFanSpeedUI()
    {
        windSpeedText.GetComponent<Text>().text = Mathf.Lerp(0.1f, 11.0f, GameController.instance.currentFanSpeed() / 10.0f).ToString("F1");
    }
    
    public void SetVisible(bool visible)
    {
        if (visible == true)
        {
            gameObject.transform.position = currentFanPosition;
            fanNoise.Play();
        }
        else
        {
            gameObject.transform.position = new Vector3(10000, 0, 0);
            fanNoise.Pause();
        }
    }

    public void UpdateFanStrength()
    {
        fanWind.UpdateFanStrength();
        SetFanSpeedUI();
    }
}
