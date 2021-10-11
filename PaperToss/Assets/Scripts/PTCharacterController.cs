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
    public MovingCharacter vampire;
    public MovingCharacter noVoiceCharacter1;

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
        dayCharacters = new List<MovingCharacter> {boss,sassy,fungirl,noVoiceCharacter1};
        nightCharacters = new List<MovingCharacter> {ghost, zombie,vampire};
    }

    public void StartWalk(MovingCharacter character)
    {
        character.WalkForward();
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
        List<MovingCharacter> charactersToChooseFrom;
        List<MovingCharacter> chosenCharacters = new List<MovingCharacter>();
        if (lightmode == LightMode.Day)
        {
            charactersToChooseFrom = new List<MovingCharacter>(dayCharacters);
        }
        else
        {
            charactersToChooseFrom = new List<MovingCharacter>(nightCharacters);
        }
        ShuffleList(charactersToChooseFrom);
        while (chosenCharacters.Count < 2)
        {
            foreach (MovingCharacter character in charactersToChooseFrom)
            {
                double cumulativeProbability = 0.0;

                if (character.ShouldIGetPicked())
                {
                    chosenCharacters.Add(character);
                }
            }
        }
        return chosenCharacters;
        
    }
}
