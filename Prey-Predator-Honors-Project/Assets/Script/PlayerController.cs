using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject Prey2;

    // Start is called before the first frame update
    void Start()
    {
        
        if (PlayerPrefs.GetFloat("PreyNum") == 2)
        {
            Prey2.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
