using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool isInBasket;
    private bool shouldFade;
    public float fadeSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFade)
        {
            // Color objectColor = this.GetComponentInChildren<Renderer>().material.color;
            // Debug.Log(objectColor);
            //
            // float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
            //
            // objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            // this.GetComponentInChildren<Renderer>().material.color = objectColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInBasket && other.CompareTag("DestroyZone"))
        {
            shouldFade = true;
            Destroy(gameObject, 2f);
        }
    }
}
