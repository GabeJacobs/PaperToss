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

    // Update is called once per frame
    void Update()
    {
      
    }

    public void PlayerScored()
    {
        score++;
        textDisplay.text = score.ToString();
    }
    public void PlayerScoredGold()
    {
        score+=3;
        textDisplay.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        textDisplay.text = "0";
    }
    

}
