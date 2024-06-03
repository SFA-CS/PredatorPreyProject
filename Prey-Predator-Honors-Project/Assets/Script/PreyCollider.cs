using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyCollider : MonoBehaviour
{
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
       

        // if a prey and predator collide notify the GameMangeer
        if (collision.gameObject.CompareTag("Predator"))
        {
            GameManager.Instance.PreyCaught(this.gameObject);
        }   
    }
}