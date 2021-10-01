using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.VFX;
using Random = System.Random;

public enum AnimationPathStyle{
    Line,
    Box,
    Hexagon
}

public class TrashCan : MonoBehaviour
{
    public GameObject glowEffect;
    public Transform waypointHolderA;
    public Transform waypointHolderB;
    private Vector3[] waypointsLine;
    private Vector3[] waypointsHexagon;
    public Transform defaultTrashPosition;
    public Transform longerWaypoint1;
    public Transform longerWaypoint2;
    public Transform longerWaypoint3;

    public bool animatingPosition;
    
    public float speed;
    public float waitTime = .3f;
    private Coroutine followPathCrt;
    private bool glowing;
    private void OnDrawGizmos()
    {
        Vector3 startPosition = waypointHolderB.GetChild(0).position;
        Vector3 prevPosition = startPosition;
        foreach (Transform waypoint in waypointHolderB)
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
        waypointsLine = new Vector3[waypointHolderA.childCount];
        for (int i = 0; i < waypointsLine.Length; i++)
        {
            waypointsLine[i] = waypointHolderA.GetChild(i).position;
        }
        waypointsHexagon = new Vector3[waypointHolderB.childCount];
        for (int i = 0; i < waypointsHexagon.Length; i++)
        {
            waypointsHexagon[i] = waypointHolderB.GetChild(i).position;
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
                    other.gameObject.transform.SetParent(gameObject.transform);
                    other.gameObject.GetComponent<Ball>().isInBasket = true;
                    if (other.GetComponent<Ball>().isGoldBall == true)
                    {
                        GameController.instance.PlayerDidScore(true); 
                    }
                    else
                    {
                        GameController.instance.PlayerDidScore(false);
                    }
                    Destroy(other.gameObject,2);
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
            glowEffect.SetActive(false);
        }
        else
        {
            gameObject.GetComponentInChildren<Renderer>().enabled = true;
            gameObject.GetComponentInChildren<Collider>().enabled = true;
            if (glowing)
            {
                glowEffect.SetActive(true);
            }
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
    
    public void StartAnimating(AnimationPathStyle pathStyle)
    {
        Vector3[] waypoints = waypointsLine;
        if(pathStyle == AnimationPathStyle.Hexagon)
        {
            waypoints = waypointsHexagon;
        }
        if (!animatingPosition)
        {
            animatingPosition = true;
            followPathCrt = StartCoroutine(FollowPath(waypoints));
        }
    }
    
    public void StopAnimating()
    {
        animatingPosition = false;
        if (followPathCrt != null)
        {
            StopCoroutine(followPathCrt);
        }
    }
    
    public void showGlow()
    {
        glowEffect.SetActive(true);
        glowing = true;
    }
    
    public void hideGlow()
    {
        glowEffect.SetActive(false);
        glowing = false;
    }

    public void MoveToLongerWaypoint(int waypoint)
    {
        if (waypoint == 1)
        {
            Debug.Log("move waypoint 1");
            transform.position = longerWaypoint1.position;
        }
        if (waypoint == 2)
        {
            Debug.Log("move waypoint 2");
            transform.position = longerWaypoint2.position;
        }     
        if (waypoint == 3)
        {
            Debug.Log("move waypoint 3");

            transform.position = longerWaypoint3.position;
        }
        
    }

    public void ResetPosition()
    {
        transform.position = defaultTrashPosition.position;
    }

}