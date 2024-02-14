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
        this.player = this.prey;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse Down " + counter);
        counter++;
        if(counter == 5)
        {
            player = this.predator;
        }
        if(counter == 10)
        {
            player = this.prey;
            counter = 0;
        }
    }
}
