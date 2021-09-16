using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelSelection : MonoBehaviour
{
    [SerializeField] private int stage;
    [SerializeField] private int level;
    [SerializeField] private bool unlocked;
    public GameObject[] stars;
    
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private GameObject numberText;
    [SerializeField] private Button button;


    private void Awake()
    {
        EventManager.StartListening("CheckLevelUnlocks", CheckUnlock);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("CheckLevelUnlocks", CheckUnlock);

        
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckUI()
    {
        Color32 darkSelection = new Color32(160, 160, 160, 255);

        if (unlocked)
        {
            ColorBlock buttonColors = button.colors;
            buttonColors.highlightedColor = darkSelection;
            button.colors = buttonColors;
            
            lockIcon.SetActive(false);
            numberText.SetActive(true);
        }
        else
        {
            ColorBlock buttonColors = button.colors;
            buttonColors.highlightedColor = buttonColors.normalColor;
            button.colors = buttonColors;
            
            lockIcon.SetActive(true);
            numberText.SetActive(false);
        }
    }

    public void StartLevel()
    {
        if (unlocked)
        {
            Debug.Log("start level");
            GameController.instance.SetUpGame(GameMode.Campaign, stage, level);
            GameController.instance.ShowInstructions();
        }
        else
        {
            
        }
    }

    private void CheckUnlock()
    {
        if (level != 1)
        {
            int unlockedInt = PlayerPrefs.GetInt(stage.ToString()+ "-" + level.ToString());
            if (unlockedInt == 1)
            {
                unlocked = true;
            }
            else
            {
                unlocked = false;
            }
            CheckUI();   
        }
    }
}
