using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanWind : MonoBehaviour
{
    [Range(15,60)]
    public float strength;
    public Vector3 direction;
}
