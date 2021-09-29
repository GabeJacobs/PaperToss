using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MovingCharacter
{

    private float turnTime = 0.6f;
    private float voiceTime = 3.0f;

    public override void StopAndDoLeftTurn()
    {
        StartCoroutine(RotateUp(Vector3.up * -90, 0.6f));
        StopWalking();
        StartCoroutine(PlayGhostClipAfterDelay(turnTime));

    }
    IEnumerator PlayGhostClipAfterDelay(float delayTime)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);
        PlayGhostVoice();
        StartCoroutine(RightTurnAfterDelay(voiceTime));
    }
    
    IEnumerator RightTurnAfterDelay(float delayTime)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(RotateUp(Vector3.up * 90, turnTime));
        yield return new WaitForSeconds(turnTime);
        movingForward = true;
    }

    void PlayGhostVoice()
    {
        Debug.Log("BOOOOOOOOooooooo");
    }

}
