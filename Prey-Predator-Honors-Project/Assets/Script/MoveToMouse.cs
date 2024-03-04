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
        player = this.prey;
        player.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;

    }

    // Update is called once per frame
    void Update()
    {
        //Input.GetMouseButtonDown(0) && selected
        if (Input.GetMouseButtonDown(0))
        {
            counter++;
            if (counter == 5)
            {
                player.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                player = this.predator; //changes camera to the predator
                player.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            if (counter == 10)
            {
                player.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                player = this.prey; //changes camera to the prey
                player.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                counter = 0;
            }

            //look to target
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            float dist = dir.magnitude;

                //move to target if its in range
            if (dist <= 30) // setting the range
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = transform.position.z;    
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
    
    //if (Input.GetMouseButtonDown(0) && selected)
    //{

    //    target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    target.z = transform.position.z;
    //}

    // transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    // }


    private void OnMouseDown()
    {
        //Sets the color of the player to blue if selected and white if not
        //selected = true;
        //gameObject.GetComponent<SpriteRenderer>().color = Color.blue;

        //foreach(MoveToMouse obj in moveableObjects)
        //{
            //if(obj != this)
            //{
                //obj.selected = false;
                //obj.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            //}
        //}
    }





}
