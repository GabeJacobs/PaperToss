using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PTCharacterController : MonoBehaviour
{
    public static PTCharacterController instance { get; private set; }
    public MovingCharacter boss;
    public MovingCharacter ghost;
    public MovingCharacter sassy;
    public MovingCharacter fungirl;
    public MovingCharacter zombie;

    private static List<MovingCharacter> dayCharacters;
    private static List<MovingCharacter> nightCharacters;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);  
        }
        DontDestroyOnLoad (gameObject);
    }

    private void Start()
    {
        dayCharacters = new List<MovingCharacter> {boss,sassy,fungirl};
        nightCharacters = new List<MovingCharacter> {ghost};
        StartWalk(zombie);
    }

    public void StartWalk(MovingCharacter character)
    {
        if (character.animator != null)
        {
            character.animator.SetBool("Idle", false);
            character.animator.SetBool("Walking", true);
        }
        else
        {
            character.WalkForward();
        }
    }
    
    public void ResetAllCharacters()
    {
        foreach (MovingCharacter character in dayCharacters)
        {
            character.Reset();
        }
        foreach (MovingCharacter character in nightCharacters)
        {
            character.Reset();
        }
    }

    private void ShuffleList(List<MovingCharacter> alpha)
    {
        for (int i = 0; i < alpha.Count; i++)
        {
            MovingCharacter temp = alpha[i];
            int randomIndex = Random.Range(i, alpha.Count);
            alpha[i] = alpha[randomIndex];
            alpha[randomIndex] = temp;
        }
    }

    public List<MovingCharacter> GetRandomChracterList(LightMode lightmode)
    {
        List<MovingCharacter> tempCharacters;
        if (lightmode == LightMode.Day)
        {
            tempCharacters = new List<MovingCharacter>(dayCharacters);
        }
        else
        {
            tempCharacters = new List<MovingCharacter>(nightCharacters);
        }
        ShuffleList(tempCharacters);
        if (tempCharacters.Count >= 3)
        {
            tempCharacters.RemoveRange(0, tempCharacters.Count-2);
        }
        return tempCharacters;
        
    }
}
