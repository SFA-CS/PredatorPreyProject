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
        Debug.Log("We are here");
        SceneManager.LoadScene("CustomGame", LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "CustomGame")
        {
            // get prey object with all the prey child objects
            GameObject preyParent = GameObject.Find("Prey");

            if (preyParent != null) // this should never be null, but just in case something bad happens so we dont crash
            {
                // get rid of preset prefabs
                foreach (Transform child in preyParent.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                // get number of prey prefabs to instantiate
                int preyCount = preyPrefabs.Length; 

                for (int i = 0; i < preyCount; i++)
                {
                    GameObject newPrey = Instantiate(preyPrefabs[preyDropdown.value], preyParent.transform);
                    newPrey.transform.SetParent(preyParent.transform);  // Set the new prey as a child of the Prey parent object
                }
            }

            // get predator object with all the predator child objects
            GameObject predatorParent = GameObject.Find("Predator");

            if (predatorParent != null) // this should never be null, but just in case something bad happens so we dont crash
            {
                // get rid of preset prefabs
                foreach (Transform child in predatorParent.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                // get number of predator prefabs to instantiate
                int predatorCount = predatorPrefabs.Length;

                for (int i = 0; i < predatorCount; i++)
                {
                    GameObject newPredator = Instantiate(predatorPrefabs[predatorDropdown.value], predatorParent.transform);
                    newPredator.transform.SetParent(predatorParent.transform);  // Set the new prey as a child of the Prey parent object
                }
            }
            
            // Unsubscribe from the event
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    /*private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "CustomGame")
        {
            // Instantiate new prefabs based on dropdown selections
            currentBackground = Instantiate(backgroundPrefabs[backgroundDropdown.value]);
            currentPrey = Instantiate(preyPrefabs[preyDropdown.value]);
            currentPredator = Instantiate(predatorPrefabs[predatorDropdown.value]);

            Debug.Log($"Background Index: {currentBackground}");
            Debug.Log($"Prey Index: {currentPrey}");
            Debug.Log($"Predator Index: {currentPredator}");


            // Unsubscribe from the event
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }*/
}
