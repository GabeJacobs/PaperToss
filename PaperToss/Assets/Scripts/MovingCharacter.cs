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
    public AudioClip[] audioClips;
    private bool beganSpeaking;
    private float turnTime = 0.6f;

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
            transform.position += transform.forward * Time.deltaTime * walkingSpeed;
        }

        if (beganSpeaking && !voice.isPlaying)
        {
            FinishedTalking();
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
        StopWalking();
        StartCoroutine(RotateMe(gameObject.transform, Vector3.up * -90 ));
        if (animator != null)
        {
            animator.SetBool("TurnLeft", true);
        }
        else
        {
            this.CallWithDelay(PlayVoice,turnTime);
        }
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
            rotateobj.transform.rotation = Quaternion.RotateTowards(rotateobj.transform.rotation, toAngle, 4f);
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
    
    public void PlayVoice()
    {
        int r = Random.Range(0, audioClips.Length-1);
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

}
