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
    public float speed = 50;
    private Vector3 target;
    private bool selected;

    // Start is called before the first frame update
    void Start()
    {
        moveableObjects.Add(this);
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
         
        if (Input.GetMouseButtonDown(0) && selected)
        {

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
        Debug.Log("Color Blue: ");
        selected = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;

        foreach(MoveToMouse obj in moveableObjects)
        {
            if(obj != this)
            {
                obj.selected = false;
                obj.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }




}
