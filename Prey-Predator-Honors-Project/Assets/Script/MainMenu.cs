using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // the number here is the sceneBuildIndex
        // we are going to use the scene name instead
        SceneManager.LoadSceneAsync(GameOptions.GetVersionName());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
