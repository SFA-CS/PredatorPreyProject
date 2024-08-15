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
            
            // replace prey prefabs
            ReplacePrefabs("Prey", preyDropdown.value, preyPrefabs);

            // replace predator prefabs
            ReplacePrefabs("Predator", predatorDropdown.value, predatorPrefabs);

            
            
            // Unsubscribe from the event
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    // to help us replace the prefabs
    // copys the original prefabs components so there isn't an issue with the components that the GameManager has references of
    private void ReplacePrefabs(string parentName, int selectedIndex, GameObject[] prefabArray)
    {
        GameObject parentObject = GameObject.Find(parentName);

        if (parentObject != null)
        {
            // Reference to the original prefab (before replacement)
            GameObject originalPrefab = null;

            if (parentObject.transform.childCount > 0)
            {
                originalPrefab = parentObject.transform.GetChild(0).gameObject;
            }

            // Clear existing children
            foreach (Transform child in parentObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            // Instantiate the new prefab
            GameObject selectedPrefab = prefabArray[selectedIndex];
            GameObject newChild = Instantiate(selectedPrefab, parentObject.transform);

            // Copy over necessary components and child objects
            if (originalPrefab != null)
            {
                // Copy Avatar component
                Avatar originalAvatar = originalPrefab.GetComponent<Avatar>();
                Avatar newAvatar = newChild.GetComponent<Avatar>();
                if (originalAvatar != null && newAvatar != null)
                {
                    CopyComponent(originalAvatar, newAvatar);
                }

                // Copy Prey Collider component
                PreyCollider originalPreyCollider = originalPrefab.GetComponent<PreyCollider>();
                PreyCollider newPreyCollider = newChild.GetComponent<PreyCollider>();
                if (originalPreyCollider != null && newPreyCollider != null)
                {
                    CopyComponent(originalPreyCollider, newPreyCollider);
                }

                // Copy LegalMoveArea child object
                Transform originalLegalMoveArea = originalPrefab.transform.Find("LegalMoveArea");
                Transform newLegalMoveArea = newChild.transform.Find("LegalMoveArea");
                if (originalLegalMoveArea != null && newLegalMoveArea != null)
                {
                    CopyTransform(originalLegalMoveArea, newLegalMoveArea);

                    // Copy MoveArea script on LegalMoveArea
                    MoveArea originalMoveArea = originalLegalMoveArea.GetComponent<MoveArea>();
                    MoveArea newMoveArea = newLegalMoveArea.GetComponent<MoveArea>();
                    if (originalMoveArea != null && newMoveArea != null)
                    {
                        CopyComponent(originalMoveArea, newMoveArea);
                    }
                }
            }
        }
    }


    // to help us copy prefab components
    private void CopyComponent<T>(T original, T destination) where T : Component
    {
        
        var type = typeof(T);
        var fields = type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        foreach (var field in fields)
        {
            field.SetValue(destination, field.GetValue(original));
        }
    }

    // to help us copy transforms
    private void CopyTransform(Transform original, Transform destination)
    {
        destination.position = original.position;
        destination.rotation = original.rotation;
        destination.localScale = original.localScale;
    }



}





