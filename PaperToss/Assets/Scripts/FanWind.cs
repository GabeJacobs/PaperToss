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
        GetFanStrength();
    }

    private void GetFanStrength()
    {
        float difficulty = ArcadeGameController.instance.gameDifficulty / 10.0f;
        strength = Mathf.Lerp(15, 60, difficulty);
    }
}
