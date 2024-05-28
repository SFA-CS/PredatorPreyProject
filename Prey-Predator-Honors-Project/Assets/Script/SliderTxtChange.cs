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
        sliderText.text = text + slider.value.ToString();
    }
}
