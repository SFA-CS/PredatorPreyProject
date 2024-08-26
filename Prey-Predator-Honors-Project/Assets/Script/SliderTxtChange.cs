using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SliderTxtChange : MonoBehaviour
{
    public string text;
    public Slider slider;
    public TMP_Text sliderText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float roundedVal = Mathf.RoundToInt(slider.value * 10) / 10.0f;
        sliderText.text = text + roundedVal;
    }
}
