using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holster : MonoBehaviour
{
    private Vector3 correctHolsterPosition;
    // Start is called before the first frame update
    void Start()
    {
        correctHolsterPosition = transform.position;
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
    }
}
