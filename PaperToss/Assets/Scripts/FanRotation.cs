using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotation : MonoBehaviour
{
    [Range(0,1)]
    public float fanStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (0,Mathf.Lerp(100,3000, fanStrength) * Time.deltaTime,0); 

    }
}
