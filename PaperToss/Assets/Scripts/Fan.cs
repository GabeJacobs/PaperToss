using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        if (currentFanPositionIndex == 0)
        {
            windSpeedText.transform.SetParent(transform.parent);
            transform.rotation = Quaternion.Euler(0, 270, 0);
            windSpeedText.transform.SetParent(windSpeedCanvas.transform);
            fanWind.direction = Vector3.left;
        }
        else
        {
            windSpeedText.transform.SetParent(transform.parent);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            windSpeedText.transform.SetParent(windSpeedCanvas.transform);
            fanWind.direction = Vector3.right;
        }

        GameObject fawnPositionWaypoint = fanSpawnObjects[currentFanPositionIndex];
        transform.position = fawnPositionWaypoint.transform.position;
        currentFanPosition = transform.position;

    }

    public void ChangeFanPosition()
    {

        placeFanInPosition(Random.Range(0,2));
    }

    public void SetFanSpeedUI()
    {
        windSpeedText.GetComponent<TextMeshProUGUI>().text = Mathf.Lerp(0.1f, 11.0f, ArcadeGameController.instance.currentFanSpeed() / 10.0f).ToString("F1");
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
    }
}
