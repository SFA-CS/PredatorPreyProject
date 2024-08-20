using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // to get prefabs for custom game
    public SpriteLoader spriteLoader;

    public void PlayGame()
    {
        // the number here is the sceneBuildIndex
        // we are going to use the scene name instead
        if (GameOptions.GetVersionName() == "CustomGame")
        {

            spriteLoader.loadSprites();
        }
        else
        {
            SceneManager.LoadSceneAsync(GameOptions.GetVersionName());
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
