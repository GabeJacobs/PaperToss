using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Transform windSource;
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
            float distanceChangecoefficient = .8f;
            float distanceFromSource = Vector3.Distance(transform.position, windSource.position);
            float distanceMultiplier = distanceChangecoefficient / distanceFromSource;
           
    
            rb.AddForce(_fanWind.direction * (_fanWind.strength * distanceMultiplier) );
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wind"))
        {
            inWindZone = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wind"))
        {
            inWindZone = false;
        }
    }
}
