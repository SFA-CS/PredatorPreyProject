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
        SceneManager.LoadScene("CustomGame", LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "CustomGame")
        {
            // Update the sprites for prey and predator
            ReplaceSprites("Prey", preyDropdown.value, preySprites);
            ReplaceSprites("Predator", predatorDropdown.value, predatorSprites);

            // Update the background sprite
            ReplaceBackgroundSprite();

            // Unsubscribe from the event
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void ReplaceSprites(string parentName, int selectedIndex, Sprite[] spriteArray)
    {
        GameObject parentObject = GameObject.Find(parentName);

        if (parentObject != null)
        {
            // Go through each child of the parent game object
            foreach (Transform child in parentObject.transform)
            {
                // Find the "Sprite" child of each "Prey" or "Predator" child object
                Transform spriteTransform = child.Find("Sprite");
                
                if (spriteTransform != null)
                {
                    // Get the sprite renderer and replace the sprite
                    SpriteRenderer spriteRenderer = spriteTransform.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null && selectedIndex >= 0 && selectedIndex < spriteArray.Length)
                    {
                        spriteRenderer.sprite = spriteArray[selectedIndex];
                        
                        // Spaceship sprites are different from animal ones and need some rotation and moving of the LegalMoveArea
                        if (spriteRenderer.sprite.name == "2DSpaceshipsFreeTrialAtlasTopViewGreen_2")
                        {
                            // Find the "LegalMoveArea" child 
                            Transform legalMoveAreaTransform = child.Find("LegalMoveArea");
                            spriteTransform.localRotation = Quaternion.Euler(0, 0, -90);
                            legalMoveAreaTransform.localPosition = new Vector3(0, 1, 0);
                            
                        }
                        // Torpedo sprites are a little different and need their scale changed
                        else if (spriteRenderer.sprite.name == "2DShipsMissilesTorpedoesAtlas_0")
                        {
                            Debug.Log("chaning torpedo local scale");
                            spriteTransform.localScale = new Vector3(0.6f, 0.4f, 1f);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }
    }

    private void ReplaceBackgroundSprite()
    {
        // Find the BackgroundCanvas GameObject first
        GameObject backgroundCanvasObject = GameObject.Find("BackgroundCanvas");

        if (backgroundCanvasObject != null)
        {
            // Find the Background GameObject under the BackgroundCanvas
            GameObject backgroundObject = backgroundCanvasObject.transform.Find("Background").gameObject;

            if (backgroundObject != null)
            {
                // Get the SpriteRenderer component
                SpriteRenderer spriteRenderer = backgroundObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // Replace the sprite with the selected one
                    spriteRenderer.sprite = backgroundSprites[backgroundDropdown.value];
                    spriteRenderer.gameObject.transform.localScale = new Vector3(1000, 1000, 1000);
                }
            }
        }
    }

}


