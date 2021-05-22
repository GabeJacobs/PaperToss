using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public bool inWindZone;
    public GameObject windZone;
    private Rigidbody rb;
    private FanWind _fanWind;

    // Start is called before the first frame update
    void Start()
    {
        _fanWind = windZone.GetComponent<FanWind>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inWindZone)
        {
            Debug.Log("force added");

            rb.AddForce(_fanWind.direction * _fanWind.strength);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wind"))
        {
            Debug.Log("in Wind Zone");
            inWindZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wind"))
        {
            Debug.Log("out of Wind Zone");

            inWindZone = false;
        }
    }
}
