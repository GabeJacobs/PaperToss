using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCharacter : MonoBehaviour
{
    public float walkingSpeed;
    public bool movingForward;
    public Animator animator;
    protected bool rotated;
    private Vector3 chatacterStartPoint;
    public AudioSource voice;

    // Start is called before the first frame update
    void Start()
    {
        chatacterStartPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingForward)
        {
            // Debug.Log("here");
            transform.position += transform.forward * Time.deltaTime * walkingSpeed;
        }
    }

    public void WalkForward()
    {
        movingForward = true;
    }

    protected void StopWalking()
    {
        if (animator != null)
        {
            animator.SetBool("Walking", false);
        }
        movingForward = false;
    }
    public virtual void StopAndDoLeftTurn()
    {
        
        StartCoroutine(RotateUp(Vector3.up * -90, 0.6f));
        if (animator != null)
        {
            animator.SetBool("TurnLeft", true);
        }
        StopWalking();
    }
    protected IEnumerator RotateUp(Vector3 byAngles, float inTime)
    {
         
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }

        rotated = true;
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
            if (animator != null)
            {
                animator.SetBool("Idle", true);
            }
            StopWalking();
            StopAllCoroutines();
            transform.position = chatacterStartPoint;
        }
    }

}
