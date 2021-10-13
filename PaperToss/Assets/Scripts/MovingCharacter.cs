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
    [Range(0.0f,1.0f)]
    public float probabilityToTalk;
    [Range(0.0f,1.0f)]
    public float probabilityToBePicked;
    private float turnTime = 0.6f;
    private bool didTurnLeft = false;

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
            animator.SetTrigger("Walk");
        }
        movingForward = true;

    }

    public void StopAndDoLeftTurn()
    {
        // Debug.Log("probability to talk is "+ probabilityToTalk);
        bool canTalk = Random.Range(0.0f, 1.0f) >= 1.0 - probabilityToTalk;
        // Debug.Log("canTalk is "+ canTalk);
        // Debug.Log("audioClips.Length"+ audioClips.Length);

        if (audioClips.Length > 0 && canTalk)
        {
            movingForward = false;
            StartCoroutine(RotateMe(gameObject.transform, Vector3.up * -90, CompletedLeftTurn));
            if (animator != null)
            {
                animator.SetTrigger("TurnLeft");
            }   
        }
    }
    public virtual void RightTurn()
    {
        StartCoroutine(RotateMe(gameObject.transform, Vector3.up * 90, CompletedRightTurn));
    }
    

    protected IEnumerator RotateMe(Transform rotateobj, Vector3 byAngles, Action callback)
    {
        Quaternion toAngle = Quaternion.Euler(rotateobj.transform.eulerAngles + byAngles);
        while (rotateobj.transform.rotation != toAngle)
        {
            rotateobj.transform.rotation = Quaternion.RotateTowards(rotateobj.transform.rotation, toAngle, 3.0f);
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
        else if (other.CompareTag("CenterTrigger") && didTurnLeft == false)
        {
            didTurnLeft = true;
            StopAndDoLeftTurn();
        } else if (other.CompareTag("EndOfRoomTrigger"))
        {
            Reset();
        }
    }
    
    public void PlayVoice()
    {
        voice.Pause();
        int r = Random.Range(0, audioClips.Length);
        voice.clip = audioClips[r];
        voice.Play();
        beganSpeaking = true;
    }

    public void Reset()
    {
        StopAllCoroutines();

        movingForward = false;
        didTurnLeft = false;
        transform.position = chatacterOriginalPosition.position;
        if (animator != null)
        {
            animator.Rebind();
            // animator.SetTrigger("Idle");
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
                animator.SetTrigger("TurnRight");
            }
            else
            {
                this.CallWithDelay(WalkForward,turnTime);
                Debug.Log("calling walk forward in 1 second");

            }
        }
    }

    void CompletedRightTurn()
    {
        this.CallWithDelay(WalkForward, .3f);
        Debug.Log("completed right turn");
    }
    
    void CompletedLeftTurn()
    {
        if (animator != null)
        {
            animator.SetTrigger("Talk");
        }
        PlayVoice();
    }

    public bool ShouldIGetPicked()
    {
        return Random.Range(0.0f, 1.0f) >= 1.0 - probabilityToBePicked;

    }
    
   
}
