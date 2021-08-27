using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighScore : MonoBehaviour
{
    public Text textDisplay;
    public int highScore = 0;
    // Start is called before the first frame update

    private void OnEnable()
    {
        EventManager.StartListening("NewHighScore", NewHighScore);

    }

    private void OnDisable()
    {
        EventManager.StopListening("NewHighScore", NewHighScore);
    }
    
    private void Awake()
    {
        textDisplay = gameObject.GetComponentInChildren<Text>();
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        textDisplay.text = "High Score: " + highScore;
    }
    
    void NewHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        textDisplay.text = "High Score: " + highScore;

        
    }

}