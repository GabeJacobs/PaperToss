using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovingCharacter : MonoBehaviour
{
    public float walkingSpeed;
    public bool movingForward;
    public Animator animator;
    public Transform chatacterOriginalPosition;
    public AudioSource voice;
    public AudioClip[] audioClips;

    private Quaternion characterOriginalRotation;
    private bool beganSpeaking;
    private float turnTime = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        characterOriginalRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movingForward)
        {
            transform.position += transform.forward * Time.deltaTime * walkingSpeed;
        }
        
        if (beganSpeaking && !voice.isPlaying)
        {
            FinishedTalking();
        }
    }

    public void WalkForward()
    {
        if (animator != null)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            movingForward = true;
        }
    }

    public void MoveStraight()
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
        StopWalking();
        StartCoroutine(RotateMe(gameObject.transform, Vector3.up * -90, CompletedLeftTurn));
        if (animator != null)
        {
            animator.SetBool("TurnLeft", true);
        }
    }
    public virtual void RightTurn()
    {
        StartCoroutine(RotateMe(gameObject.transform, Vector3.up * 90, CompletedRightTurn));
    }
    

    protected IEnumerator RotateMe(Transform rotateobj, Vector3 byAngles, Action callback)
    {
        //Quaternion fromAngle = rotateobj.transform.rotation;
        Quaternion toAngle = Quaternion.Euler(rotateobj.transform.eulerAngles + byAngles);

        while (rotateobj.transform.rotation != toAngle)
        {
            rotateobj.transform.rotation = Quaternion.RotateTowards(rotateobj.transform.rotation, toAngle, 4f);
            yield return null;
        }

        if(callback != null) callback();
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
    
    public void PlayVoice()
    {
        
        int r = Random.Range(0, audioClips.Length);
        voice.clip = audioClips[r];
        voice.Play();
        beganSpeaking = true;
    }

    public void Reset()
    {
        StopWalking();
        StopAllCoroutines();
        transform.position = chatacterOriginalPosition.position;
        if (animator != null)
        {
            animator.Rebind();
            animator.SetBool("Idle", true);

        }
    }
    
    public void ResetParams()
    {
        if (animator != null)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("TurnLeft", false);
            animator.SetBool("TurnRight", false);
            animator.SetBool("Walking", false);
            animator.SetBool("FinishedTalking", false);
        }
    }
    
    public void FinishedTalking()
    {
       this.CallWithDelay(PerformFinishedTalking, 1.0f);
    }

    private void PerformFinishedTalking()
    {
        if (beganSpeaking == true)
        {
            beganSpeaking = false;
            RightTurn();
            if (animator != null)
            {
                animator.SetBool("FinishedTalking", true);
                animator.SetBool("TurnLeft", false);
            }
            else
            {
                this.CallWithDelay(WalkForward,turnTime);
            }
        }
    }

    void CompletedRightTurn()
    {
        WalkForward();
    }
    
    void CompletedLeftTurn()
    {
        if (animator == null)
        {
            PlayVoice();
        }
    }
}
