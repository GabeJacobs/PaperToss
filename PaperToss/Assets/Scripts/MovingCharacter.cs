using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCharacter : MonoBehaviour
{
    public float walkingSpeed;
    public bool movingForward;
    public Animator animator;
    private bool rotated;

    // Start is called before the first frame update
    void Start()
    {
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
    public void StopAndDoLeftTurn()
    {
        
        movingForward = false;
        StartCoroutine(RotateUp(Vector3.up * -90, 0.6f));
        animator.SetBool("Walking", false);
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
        rotated = true;
    }

    public void FinishedAngryPoint()
    {
        animator.SetBool("TurnLeft", false);
        animator.SetBool("TurnRight", true);
        StartCoroutine(RotateUp(Vector3.up * 90, 0.6f));

    }

}
