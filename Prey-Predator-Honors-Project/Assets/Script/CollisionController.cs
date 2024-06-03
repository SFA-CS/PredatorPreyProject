using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionController : MonoBehaviour
{
    public GameOverScreen GameOverScreen;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Predator Sprite" || collision.gameObject.name == "Prey Sprite") //if either sprite runs into each other
        {
            Debug.Log("Prey is Dead"); //Test for collider working
            GameOverScreen.Display(""); //Game Over Screen is Shown
        }
    }

    
}
