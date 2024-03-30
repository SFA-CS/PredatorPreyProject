using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;

    private int numTurns;
    public int NumTurns { get { return numTurns; } set { numTurns = value; updateScoreBoard();  } }

    private int preyRemaining;
    public int PreyRemaining { get { return preyRemaining; } set {  preyRemaining = value; updateScoreBoard(); } }

    private int maxTurns;
    public int MaxTurns {  get { return maxTurns; } set {  maxTurns = value; updateScoreBoard(); } }

    // Singleton Design Pattern
    public static Scoreboard Instance { get; private set; }

    private void Awake()
    {
        // singleton design pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        this.m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void updateScoreBoard()
    {
        string text = "Turn: " + this.numTurns + " of " + this.maxTurns + "<br>";
        text += "Prey Remaining: " + this.preyRemaining;
        this.m_TextMeshProUGUI.SetText(text);
    }

}
