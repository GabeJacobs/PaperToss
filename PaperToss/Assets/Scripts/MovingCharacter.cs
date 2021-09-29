using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCharacter : MonoBehaviour
{
    public float walkingSpeed;
    public bool movingForward;
    public Animator animator;
    private Vector3 chatacterStartPoint;

    // Start is called before the first frame update
    void Start()
    {
        chatacterStartPoint = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingForward)
        {
            transform.position += transform.forward * Time.deltaTime * walkingSpeed;
        }
    }

    public void WalkForward()
    {
        movingForward = true;
    }

    public void StopWalking()
    {
        animator.SetBool("Walking", false);
        movingForward = false;
    }
    public void StopAndDoLeftTurn()
    {
        
        StartCoroutine(RotateUp(Vector3.up * -90, 0.6f));
        StopWalking();
        animator.SetBool("TurnLeft", true);
    }
    IEnumerator RotateUp(Vector3 byAngles, float inTime)
    {
         
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

    public void FinishedAngryPoint()
    {
        animator.SetBool("TurnLeft", false);
        StartCoroutine(RotateUp(Vector3.up * 90, 0.6f));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestroyZone"))
        {
            return;
        }
        else if (other.CompareTag("CenterTrigger"))
        {
            StopAndDoLeftTurn();
        } else if (other.CompareTag("EndOfRoomTrigger"))
        {
            animator.SetBool("Idle", true);
            StopWalking();
            transform.position = chatacterStartPoint;
            StopAllCoroutines();
            animator.SetBool("Walking", true);
            animator.SetBool("Idle", false);

        }
    }

}
