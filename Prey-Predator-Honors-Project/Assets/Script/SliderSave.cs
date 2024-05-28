using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSave : MonoBehaviour
{
    public Slider PreyNumSlider;
    public Slider PredNumSlider;
    public Slider TurnNumSlider;
    public Slider PreyRadSlider;
    public Slider PredRadSlider;
    public Slider PreyTravDistSlider;
    public Slider PredTravDistSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("PreyNum", (int)PreyNumSlider.value);
        PlayerPrefs.SetInt("PredNum", (int)PredNumSlider.value);
        PlayerPrefs.SetInt("TurnNum", (int)TurnNumSlider.value);
        PlayerPrefs.SetFloat("PreyRad", PreyRadSlider.value);
        PlayerPrefs.SetFloat("PredRad", PredRadSlider.value);
        PlayerPrefs.SetFloat("PreyTravDist", PreyTravDistSlider.value);
        PlayerPrefs.SetFloat("PredTravDist", PredTravDistSlider.value);
    }
}
