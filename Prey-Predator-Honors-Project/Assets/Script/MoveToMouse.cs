using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class MoveToMouse : MonoBehaviour
{
    public static List<MoveToMouse> moveableObjects = new List<MoveToMouse>();
    public Transform predator;
    public Transform prey;
    private Transform player;
    public float speed = 50;
    private Vector3 target;
    private bool selected;
    int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        moveableObjects.Add(this);
        target = transform.position;
        player = this.prey; //sets the first player to be prey
        player.gameObject.GetComponent<SpriteRenderer>().color = Color.blue; //sets the prey to blue

    }

    // Update is called once per frame
    void Update()
    {
        //Input.GetMouseButtonDown(0) && selected
        if (Input.GetMouseButtonDown(0))
        {
            counter++;
            if (counter == 1)
            {
                player.gameObject.GetComponent<SpriteRenderer>().color = Color.white; //sets the prey to white
                player = this.predator; //changes camera to the predator
                player.gameObject.GetComponent<SpriteRenderer>().color = Color.red; //sets the predator to red
            }
            if (counter == 2)
            {
                player.gameObject.GetComponent<SpriteRenderer>().color = Color.white; //sets the predator to white
                player = this.prey; //changes camera to the prey
                player.gameObject.GetComponent<SpriteRenderer>().color = Color.blue; //sets the prey to blue
                counter = 0;
            }

            if(player.gameObject.GetComponent<SpriteRenderer>().color == Color.blue || player.gameObject.GetComponent<SpriteRenderer>().color == Color.red)
            {
                //look to target
                Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 dir = Input.mousePosition - pos;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                float dist = dir.magnitude;

                if (dist <= 30) // setting the range
                {
                    //Have to be in the range to move to the clicked point
                    target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    target.z = transform.position.z;
                }
            }

                
        }
        //move to target if its in range
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {

    }





}
