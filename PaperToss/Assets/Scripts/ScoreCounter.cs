using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreCounter : MonoBehaviour
{
    public Text textDisplay;
    public int score = 0;
    // Start is called before the first frame update

    private void OnEnable()
    {
        EventManager.StartListening("PlayerScored", PlayerScored);

    }

    private void OnDisable()
    {
        EventManager.StopListening("PlayerScored", PlayerScored);
    }

    
    private void Awake()
    {
        textDisplay = gameObject.GetComponentInChildren<Text>();
    }

    void Start()
    {
        textDisplay.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void PlayerScored()
    {
        score++;
        textDisplay.text = score.ToString();

    }

}
