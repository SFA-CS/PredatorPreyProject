using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI WinnerText;
    public static GameOverScreen Instance { get; private set; }

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
    }

    public void Display(string message)
    {
        WinnerText.text = message;
        gameObject.transform.GetChild(0).gameObject.SetActive(true); //Predator Win screen is set to active aka its now visable
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("PredatorPrey"); //if click on restart button game is reset
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu"); //if click on main menu button you are sent to the menu
    }
}
