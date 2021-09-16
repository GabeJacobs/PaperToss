using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanWind : MonoBehaviour
{
    [Range(15,60)]
    public float strength;
    public Vector3 direction;

    private void Start()
    {
        UpdateFanStrength();
    }

    public void UpdateFanStrength()
    {
        float difficulty = GameController.instance.currentFanSpeed() / 10.0f;
        strength = Mathf.Lerp(10, 60, difficulty);
    }
}
