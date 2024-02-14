using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    int counter = 0;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
        
    }

    public void onMouseDown()
    {
        counter++;
        if(counter == 5)
        {
            player = transform.Find("Predator Sprite");
        }
        if(counter == 10)
        {
            player = transform.Find("Prey Sprite");
            counter = 0;
        }
    }
}
