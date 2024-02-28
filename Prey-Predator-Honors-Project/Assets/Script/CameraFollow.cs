using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class CameraFollow : MonoBehaviour 
{
    public Transform predator;
    public Transform prey;
    private Transform player;
    public Vector3 offset;
    int counter = 0;

    void Start()
    {
        this.player = this.prey; //sets camera on the prey at the start
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //counts up at every click, and if they click 5 times the camera switches to the other player
            counter++;
            if (counter == 5)
            {
                player = this.predator; //changes camera to the predator
            }
            if (counter == 10)
            {
                player = this.prey; //changes camera to the prey
                counter = 0;
            }
            //Moves the camera when the player moves
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
        }
    }

    private void OnMouseDown()
    {
        
    }
}
