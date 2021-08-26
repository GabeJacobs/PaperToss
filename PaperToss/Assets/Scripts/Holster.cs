using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class Holster : MonoBehaviour
{
    private Vector3 correctHolsterPosition;
    public bool isVisible;
    public SnapZone SnapZone;

    // Start is called before the first frame update
    void Start()
    {
        correctHolsterPosition = transform.position;
        SnapZone = gameObject.GetComponentInChildren<SnapZone>();
        SetVisible(false);
    }
    
    
    public void SetVisible(bool visible)
    {
        if (visible == true)
        {
            gameObject.transform.position = correctHolsterPosition;
        }
        else
        {
            gameObject.transform.position = new Vector3(10000, 0, 0);
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
}
