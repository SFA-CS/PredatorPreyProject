using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour
{
    public const string PREY_NUMBER = "PreyNum";
    public const string PREDATOR_NUMBER = "PredNum";
    public const string TURNS = "TurnNum";
    public const string PREY_RADIUS = "PreyRad";
    public const string PREDATOR_RADIUS = "PredRad";
    public const string PREY_DISTANCE = "PreyTravDist";
    public const string PREDATOR_DISTANCE = "PredTravDist";
    public const string PROXIMITY = "PROXIMITY";

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
        // TODO: SET DEFAULTS
    }

    // Update is called once per frame
    void Update()
    {   PlayerPrefs.SetFloat("", PredTravDistSlider.value);
    }

    public void SetNumberOfPrey(float num)
    {
        PlayerPrefs.SetInt(PREY_NUMBER, (int) num);
    }

    public void SetNumberOfPredators(float num)
    {
        PlayerPrefs.SetInt(PREDATOR_NUMBER, (int) num);

    }

    public void SetNumberOfTurns(float num)
    {
        PlayerPrefs.SetInt(TURNS, (int) num);
    }

    public void SetPreyRadius(float radius)
    {
        PlayerPrefs.SetFloat(PREY_RADIUS, radius);

    }

    public void SetPredatorRadius(float radius)
    {
        PlayerPrefs.SetFloat(PREDATOR_RADIUS, radius);
    }

    public void SetPreyMaxTravelDistance(float  distance)
    {
        PlayerPrefs.SetFloat(PREY_DISTANCE, PreyTravDistSlider.value);
    }

    public void SetPredatorMaxTravelDistance(float distance)
    {
        PlayerPrefs.SetFloat(PREDATOR_DISTANCE, PreyTravDistSlider.value);
    }

    public void SetProximity(int proximity)
    {
        PlayerPrefs.SetInt(PROXIMITY, proximity);
    }
}
