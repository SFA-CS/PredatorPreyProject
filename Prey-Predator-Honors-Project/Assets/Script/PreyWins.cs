using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreyWins : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true); //Prey Wins screen is set to active aka its now visable
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("BearRacoonGame"); //if click on restart button game is reset
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu"); //if click on main menu button you are sent to the menu
    }
}
