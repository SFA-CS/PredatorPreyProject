using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PrefabLoader : MonoBehaviour
{
    public TMP_Dropdown backgroundDropdown;
    public TMP_Dropdown preyDropdown;
    public TMP_Dropdown predatorDropdown;

    public GameObject[] backgroundPrefabs;
    public GameObject[] preyPrefabs;
    public GameObject[] predatorPrefabs;

    private GameObject currentBackground;
    private GameObject currentPrey;
    private GameObject currentPredator;

    public void LoadPrefabs()
    {
        SceneManager.LoadScene("CustomGame", LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "CustomScene")
        {
            // Instantiate new prefabs based on dropdown selections
            currentBackground = Instantiate(backgroundPrefabs[backgroundDropdown.value]);
            currentPrey = Instantiate(preyPrefabs[preyDropdown.value]);
            currentPredator = Instantiate(predatorPrefabs[predatorDropdown.value]);

            // Unsubscribe from the event
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
