using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI.ProceduralImage;

public class ButtonHighlight : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Color32 defaultColor;
    public float defaultBorderWidth;
    public Color32 highlightedColor;

    private void Start()
    {
        // defaultColor = gameObject.GetComponent<ProceduralImage>().color;
    }

    // When highlighted with mouse.
    public void OnPointerEnter(PointerEventData eventData)
    {
        // defaultColor = gameObject.GetComponent<ProceduralImage>().color;
        // defaultBorderWidth = gameObject.GetComponent<ProceduralImage>().BorderWidth;
        //
        // gameObject.GetComponent<ProceduralImage>().color = highlightedColor;
        // // gameObject.GetComponent<ProceduralImage>().BorderWidth = 0;

    }

    // When selected.
    public void OnPointerExit(PointerEventData eventData)
    {
        // gameObject.GetComponent<ProceduralImage>().color = defaultColor;
        // gameObject.GetComponent<ProceduralImage>().BorderWidth = defaultBorderWidth;
    }

    public void OnSelect(BaseEventData eventData)
    {
        
    }
}