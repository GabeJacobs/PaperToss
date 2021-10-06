using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreCounter : MonoBehaviour
{
    public Text textDisplay;
    public int score = 0;
    // Start is called before the first frame update
    
    private void Awake()
    {
        textDisplay = gameObject.GetComponentInChildren<Text>();
    }

    void Start()
    {
            ResetScore();
    }

    public void PlayerScored(bool gold, bool fire)
    {
        if (gold)
        {
            score+=3;
        } else if (fire)
        {
            score+=5;
        }
        else
        {
            score++;
        }
        textDisplay.text = score.ToString();
    }
    public void ResetScore()
    {
        score = 0;
        textDisplay.text = "0";
    }
    

}
