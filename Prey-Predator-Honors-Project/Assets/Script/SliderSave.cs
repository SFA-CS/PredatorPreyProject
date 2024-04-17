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
        PlayerPrefs.SetFloat("PreyNum",  PreyNumSlider.value);
        PlayerPrefs.SetFloat("PredNum", PredNumSlider.value);
        PlayerPrefs.SetFloat("TurnNum", TurnNumSlider.value);
        PlayerPrefs.SetFloat("PreyRad", PreyRadSlider.value);
        PlayerPrefs.SetFloat("PredRad", PredRadSlider.value);
        PlayerPrefs.SetFloat("PreyTravDist", PreyTravDistSlider.value);
        PlayerPrefs.SetFloat("PredTravDist", PredTravDistSlider.value);
    }
}
