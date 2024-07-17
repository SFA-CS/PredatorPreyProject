using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GameOptions;

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
    public enum Proximity { Close=0, Mid=1, Far=2 };
    public const string VERSION = "VERSION";

    public enum Version { BearDeer = 0, BearFox = 1, BearRacoon = 2, MissileShip = 3, TigerDeer = 4, TigerFox = 5, TigerRacoon = 6 };
    public static readonly string[] VersionName = { "BearDeerGame", "BearFoxGame", "BearRacoonGame", "MissileShipGame", "TigerDeerGame", "TigerFoxGame", "TigerRacoonGame" };

    [SerializeField]
    private Slider PreyNumSlider;

    [SerializeField]
    private Slider PredNumSlider;

    [SerializeField]
    private Slider TurnNumSlider;

    [SerializeField]
    private Slider PreyRadSlider;

    [SerializeField]
    private Slider PredRadSlider;

    [SerializeField]
    private Slider PreyTravDistSlider;

    [SerializeField]
    private  Slider PredTravDistSlider;

    [SerializeField]
    private TMP_Dropdown DistanceDropDown;

    [SerializeField]
    private TMP_Dropdown VersionDropDown;
    // Start is called before the first frame update


    void Start()
    {
        PlayerPrefs.SetInt(PREY_NUMBER,  3);
        this.PreyNumSlider.SetValueWithoutNotify(3);
        
        PlayerPrefs.SetInt(PREDATOR_NUMBER, 2);
        this.PredNumSlider.SetValueWithoutNotify(2);
        
        PlayerPrefs.SetInt(TURNS, 10);
        this.TurnNumSlider.SetValueWithoutNotify(10);
        
        PlayerPrefs.SetFloat(PREY_RADIUS, 1.0f);
        this.PreyRadSlider.SetValueWithoutNotify(1.0f);
        
        PlayerPrefs.SetFloat(PREDATOR_RADIUS, 2.0f);
        this.PredRadSlider.SetValueWithoutNotify(2.0f);

        PlayerPrefs.SetFloat(PREY_DISTANCE, 1.5f);
        this.PreyTravDistSlider.SetValueWithoutNotify(1.5f);

        PlayerPrefs.SetFloat(PREDATOR_DISTANCE, 2.5f);
        this.PredTravDistSlider.SetValueWithoutNotify(2.5f);

        PlayerPrefs.SetInt(PROXIMITY, (int) Proximity.Mid);
        this.DistanceDropDown.SetValueWithoutNotify((int)Proximity.Mid);

        PlayerPrefs.SetInt(VERSION, (int)Version.MissileShip);
        this.VersionDropDown.SetValueWithoutNotify((int)(Version.MissileShip));
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
        PlayerPrefs.SetFloat(PREY_DISTANCE, distance);
    }

    public void SetPredatorMaxTravelDistance(float distance)
    {
        PlayerPrefs.SetFloat(PREDATOR_DISTANCE, distance);
    }

    public void SetProximity(int proximity)
    {   
        PlayerPrefs.SetInt(PROXIMITY, proximity);
    }

    public void setVersion(int version)
    {
        PlayerPrefs.SetInt(VERSION, version);
    }

    public static string GetVersionName()
    {
        return VersionName[PlayerPrefs.GetInt(VERSION)];
    }
}
