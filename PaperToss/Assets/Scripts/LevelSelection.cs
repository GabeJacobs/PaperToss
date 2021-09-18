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
    [SerializeField] private bool completed;

    public GameObject[] stars;
    
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private GameObject numberText;
    [SerializeField] private Button button;
    [SerializeField] private GameObject completedIcon;


    private void Awake()
    {
        EventManager.StartListening("CheckLevelUnlocks", CheckUnlock);
        EventManager.StartListening("CheckLevelCompletes", CheckComplete);

    }

    private void OnDestroy()
    {
        EventManager.StopListening("CheckLevelUnlocks", CheckUnlock);
        EventManager.StopListening("CheckLevelCompletes", CheckComplete);

    }

    // Start is called before the first frame update
    void Start()
    {
        CheckUnlock();
        CheckComplete();
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

        if (completed)
        {
            completedIcon.SetActive(true);
        }
        else
        {
            completedIcon.SetActive(false);
        }
    }

    public void StartLevel()
    {
        if (unlocked)
        {
            GameController.instance.SetUpGame(GameMode.Campaign, stage, level);
            GameController.instance.ShowInstructions();
        }
        else
        {
            
        }
    }

    private void CheckUnlock()
    {
        if (stage > 1 || level > 1)
        {
            int unlockedInt = PlayerPrefs.GetInt(stage.ToString()+ "-" + level.ToString());
            unlocked = (unlockedInt == 1);
            // Debug.Log(stage.ToString()+ "-" + level.ToString() + " --- " + unlocked);
            // Debug.Log(stage.ToString()+ "-" + level.ToString() + " --- " + unlockedInt);
            CheckUI();   
        }
    }
    private void CheckComplete()
    {
        int completeInt = PlayerPrefs.GetInt(stage.ToString()+ "-" + level.ToString()+"complete");
        completed = (completeInt == 1);
        // Debug.Log(stage.ToString()+ "-" + level.ToString() + " --- " + completed);
        // Debug.Log(stage.ToString()+ "-" + level.ToString() + " --- " + completeInt);
        CheckUI();
    }
    
}
