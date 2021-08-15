using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPlacement : MonoBehaviour
{
    private GameObject[] fanSpawnObjects;
    public int currentFanPosition = 1;

    private void OnEnable()
    {
        TrashCan.onScoreEvent += scoreEventRecieved;
    }

    private void OnDisable()
    {
        TrashCan.onScoreEvent -= scoreEventRecieved;
    }

    // Start is called before the first frame update
    void Start()
    {
        fanSpawnObjects = GameObject.FindGameObjectsWithTag("FanSpawnPoint");
        placeFanInPosition(1);
    }

    private void placeFanInPosition(int position)
    {
        currentFanPosition = position;
        Vector3 direction = Vector3.left;
    
        if (currentFanPosition % 2 == 1) // on right side
        {
            direction = Vector3.right;
            var transform1 = transform;
            transform1.eulerAngles = new Vector3(transform1.eulerAngles.x, 90f, transform1.eulerAngles.z);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 270f, transform.eulerAngles.z);
        }

        gameObject.GetComponentInChildren<FanWind>().direction = direction;
        // place position
        GameObject fawnPositionWaypoint = fanSpawnObjects[currentFanPosition];
        transform.position = fawnPositionWaypoint.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void scoreEventRecieved()
    {
        int placementPosition = UnityEngine.Random.Range(0, 4);
        placeFanInPosition(placementPosition);

    }
}
