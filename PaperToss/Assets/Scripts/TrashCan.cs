using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class TrashCan : MonoBehaviour
{

    public Transform waypointHolderA;
    public bool oscilating;
    public float speed = 5;
    public float waitTime = .3f;
    public Transform defaultPositon;
    private Vector3[] waypointsA;

    private void OnDrawGizmos()
    {
        Vector3 startPosition = waypointHolderA.GetChild(0).position;
        Vector3 prevPosition = startPosition;
        foreach (Transform waypoint in waypointHolderA)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(prevPosition, waypoint.position);
            prevPosition = waypoint.position;
        }
        Gizmos.DrawLine(prevPosition, startPosition);
    }

    private void Start()
    {
        SetVisible(false);
        waypointsA = new Vector3[waypointHolderA.childCount];
        for (int i = 0; i < waypointsA.Length; i++)
        {
            waypointsA[i] = waypointHolderA.GetChild(i).position;
        }
    }
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other) {
        if (GameController.instance.gameIsRunning && !GameController.instance.gameIsPaused)
        {
            if (other.tag == "Ball")
            {
                if (!other.gameObject.GetComponent<Ball>().isInBasket)
                {
                    if (other.GetComponent<Ball>().isGoldBall == true)
                    {
                        GameController.instance.PlayerDidScore(true); 
                        other.gameObject.GetComponent<Ball>().isInBasket = true;
                     
                    }
                    else
                    {
                        GameController.instance.PlayerDidScore(false);
                        other.gameObject.GetComponent<Ball>().isInBasket = true;
                    }
                }
            }
        }
    }

    public void SetVisible(bool isVisible)
    {
        if (isVisible == false)
        {
            gameObject.GetComponentInChildren<Renderer>().enabled = false;
            gameObject.GetComponentInChildren<Collider>().enabled = false;

        }
        else
        {
            gameObject.GetComponentInChildren<Renderer>().enabled = true;
            gameObject.GetComponentInChildren<Collider>().enabled = true;
        }
    }

    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];
        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position,targetWaypoint, speed * Time.deltaTime);
            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
            }
            yield return null;
        }
    }
    
    public void BeginOscilation()
    {
        if (!oscilating)
        {
            oscilating = true;
            StartCoroutine(FollowPath(waypointsA));
        }
    }
    
    public void StopOscilation()
    {
        oscilating = false;
        StopCoroutine(FollowPath(waypointsA));
        transform.position = defaultPositon.position;
    }

}