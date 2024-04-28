using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject Prey1;
    public GameObject Prey2;
    public GameObject Prey3;
    public GameObject Prey4;
    public GameObject Prey5;

    public GameObject Pred1;
    public GameObject Pred2;
    public GameObject Pred3;
    public GameObject Pred4;
    public GameObject Pred5;


    // Start is called before the first frame update
    void Start()
    {
        //Prey Setups
        if (PlayerPrefs.GetInt("PreyNum") == 2)
        {
            Prey1.SetActive(false);
            Prey2.SetActive(true);
            Prey3.SetActive(true);
        }
        if (PlayerPrefs.GetInt("PreyNum") == 3)
        {
            Prey2.SetActive(true);
            Prey3.SetActive(true);
        }
        if (PlayerPrefs.GetInt("PreyNum") == 4)
        {
            Prey1.SetActive(false);
            Prey2.SetActive(true);
            Prey3.SetActive(true);
            Prey4.SetActive(true);
            Prey5.SetActive(true);
        }
        if (PlayerPrefs.GetInt("PreyNum") == 5)
        {
            Prey2.SetActive(true);
            Prey3.SetActive(true);
            Prey4.SetActive(true);
            Prey5.SetActive(true);
        }
        //Predator Setups
        if (PlayerPrefs.GetInt("PredNum") == 2)
        {
            Pred1.SetActive(false);
            Pred2.SetActive(true);
            Pred3.SetActive(true);
        }
        if (PlayerPrefs.GetInt("PredNum") == 3)
        {
            Pred2.SetActive(true);
            Pred3.SetActive(true);
        }
        if (PlayerPrefs.GetInt("PredNum") == 4)
        {
            Pred1.SetActive(false);
            Pred2.SetActive(true);
            Pred3.SetActive(true);
            Pred4.SetActive(true);
            Pred5.SetActive(true);
        }
        if (PlayerPrefs.GetInt("PredNum") == 5)
        {
            Pred2.SetActive(true);
            Pred3.SetActive(true);
            Pred4.SetActive(true);
            Pred5.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
