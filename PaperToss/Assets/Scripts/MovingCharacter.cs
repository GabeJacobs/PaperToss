using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCharacter : MonoBehaviour
{
    public float walkingSpeed;
    public bool movingForward;
    public Animator animator;
    protected bool rotated;
    public Transform chatacterOriginalPosition;
    private Quaternion characterOriginalRotation;

    public AudioSource voice;

    // Start is called before the first frame update
    void Start()
    {
        // chatacterOriginalPosition = transform.position;
        characterOriginalRotation = transform.rotation;
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
        if (animator != null)
        {
            animator.SetBool("Walking", true);
        }
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
        
        StartCoroutine(RotateMe(gameObject.transform, Vector3.up * -90 ));
        if (animator != null)
        {
            animator.SetBool("TurnLeft", true);
        }
        StopWalking();
    }
    public virtual void RightTurn()
    {
        StartCoroutine(RotateMe(gameObject.transform, Vector3.up * 90 ));
    }
    
 
    Quaternion toAngle;

    protected IEnumerator RotateMe(Transform rotateobj, Vector3 byAngles)
    {


        //Quaternion fromAngle = rotateobj.transform.rotation;

        toAngle = Quaternion.Euler(rotateobj.transform.eulerAngles + byAngles);

        while (rotateobj.transform.rotation != toAngle)
        {
            rotateobj.transform.rotation = Quaternion.RotateTowards(rotateobj.transform.rotation, toAngle, 7f);
            yield return null;
        }
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
            Reset();
        }
    }

    public void Reset()
    {
        StopWalking();
        StopAllCoroutines();
        transform.position = chatacterOriginalPosition.position;
        // transform.rotation = characterOriginalRotation;
        if (animator != null)
        {
            animator.Rebind();
            animator.SetBool("Idle", true);
        }

    }

}
