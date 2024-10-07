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
    private SpriteLoader spriteLoader;

    
    private Vector3[] preyTransforms = { new Vector3(-5, 0, 0), new Vector3(-8, 2, 0), new Vector3(-8, -2, 0) };
    private Vector3[] predatorTransforms = { new Vector3(5, 0, 0), new Vector3(8, 2, 0), new Vector3(8, -2, 0) };

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
        //SceneManager.LoadScene("CustomGame"); //if click on restart button game is reset
        movePredatorToStart();
        movePreyToStart();

    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu"); //if click on main menu button you are sent to the menu
    }

    private void movePredatorToStart()
    {
        GameObject PredatorParent = GameObject.Find("Predator");
        Debug.Log("Here");
        int numToActivate = PlayerPrefs.GetInt(GameOptions.PREDATOR_NUMBER);
        int i = 0;
        // Go through each child of the parent game object
        foreach (Transform child in PredatorParent.transform)
        {
            child.gameObject.SetActive(true);
            child.localPosition = predatorTransforms[i];
            child.localRotation = Quaternion.Euler(0, 0, 90);
            numToActivate--;

            if(numToActivate <= 0)
            {
                break;
            }
        }
    }

    private void movePreyToStart()
    {
        GameObject PreyParent = GameObject.Find("Prey");
        Debug.Log("Here1");
        int numToActivate = PlayerPrefs.GetInt(GameOptions.PREY_NUMBER);
        int i = 0;
        Debug.Log("Here2");
        // Go through each child of the parent game object
        foreach (Transform child in PreyParent.transform)
        {
            child.gameObject.SetActive(true);
            child.localPosition = preyTransforms[i];
            child.localRotation = Quaternion.Euler(0, 0, -90);
            numToActivate--;

            if (numToActivate <= 0)
            {
                break;
            }
            i++;
        }

        
    }
}
