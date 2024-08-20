using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SpriteLoader : MonoBehaviour
{
    public TMP_Dropdown backgroundDropdown;
    public TMP_Dropdown preyDropdown;
    public TMP_Dropdown predatorDropdown;

    public Sprite[] backgroundSprites;
    public Sprite[] preySprites;
    public Sprite[] predatorSprites;

    public void LoadSprites()
    {
        Debug.Log("We are here");
        SceneManager.LoadScene("CustomGame", LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "CustomGame")
        {
            // Update the sprites for background, prey, and predator
            ReplaceSprite("Background", backgroundDropdown.value, backgroundSprites);
            ReplaceSprite("Prey", preyDropdown.value, preySprites);
            ReplaceSprite("Predator", predatorDropdown.value, predatorSprites);

            // Unsubscribe from the event
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void ReplaceSprite(string parentName, int selectedIndex, Sprite[] spriteArray)
    {
        GameObject parentObject = GameObject.Find(parentName);

        if (parentObject != null)
        {
            // Find the "Sprite" child of the object
            Transform spriteTransform = parentObject.transform.Find("Sprite");
            if (spriteTransform != null)
            {
                SpriteRenderer spriteRenderer = spriteTransform.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && selectedIndex >= 0 && selectedIndex < spriteArray.Length)
                {
                    spriteRenderer.sprite = spriteArray[selectedIndex];
                }
            }
        }
    }
}
