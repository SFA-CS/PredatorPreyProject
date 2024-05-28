using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    public int score;
    private int count = 0;
    public PreyWins PreyWins;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score; //sets the text to the right score
        if (Input.GetMouseButtonDown(0)) //If the user clicks it adds 1 to the score
        {
            count++;
            if (count%2 == 1) //count is in place so that only when the prey moves, the score is increased
            {
                score++;
            }
            
        }
        if(score == PlayerPrefs.GetInt("TurnNum")) //if score is what the user selected in settings the Prey Wins screen is displayed
        {
            PreyWins.Setup(); 
        }
    }
}
