using System;
using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;
using UnityEngine.UI;

public class Holster : MonoBehaviour
{
    private Vector3 rightHolsterPosition;
    private Vector3 leftHolsterPosition;
    private Vector3 currentHolsterPosition;

    public bool isVisible;
    public bool isLefty;
    public SnapZone SnapZone;


    // Start is called before the first frame update
    void Start()
    {
        rightHolsterPosition = transform.position;
        leftHolsterPosition = new Vector3(0.7f, 0, 0);
        SnapZone = gameObject.GetComponentInChildren<SnapZone>();
        SetVisible(false);
    }
    
    
    public void SetVisible(bool visible)
    {
        Component[] renderers = gameObject.GetComponentsInChildren(typeof(Renderer));
        Component[] texts = gameObject.GetComponentsInChildren(typeof(Text));
        Component[] colliders = gameObject.GetComponentsInChildren(typeof(SphereCollider));

        foreach (Component renderer in renderers)
        {
            Renderer c = (Renderer) renderer;
            c.enabled = visible;
        }

        foreach (Component text in texts)
        {
            Text c = (Text) text;
            c.enabled = visible;
        }
        
        foreach (Component collider in colliders)
        {
            SphereCollider c = (SphereCollider) collider;
            c.enabled = visible;
        }
        isVisible = visible;
    }

    public void SetNextBallToBeGold()
    {
        SnapZone.nextBallIsGold = true;
    }
    public void SetNextBallToBeNormal()
    {
        SnapZone.nextBallIsGold = false;
    }
    
    public void SetHolsterPosition()
    {
        if (isLefty != Convert.ToBoolean(PlayerPrefs.GetInt("isLefty")))
        {
            Debug.Log("changed hands");
            isLefty = Convert.ToBoolean(PlayerPrefs.GetInt("isLefty"));
            if (isLefty == true)
            {
                Debug.Log("set to left");
                currentHolsterPosition = leftHolsterPosition;
            }
            else
            {
                Debug.Log("set to right");
                currentHolsterPosition = Vector3.zero;

            }
            gameObject.transform.localPosition = currentHolsterPosition;
        }
        
    }
}
