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
    public enum Version { Custom = 0, BearDeer = 1, BearFox = 2, BearRacoon = 3, MissileShip = 4, TigerDeer = 5, TigerFox = 6, TigerRacoon = 7 }; // choices in dropdown
    public static readonly string[] VersionName = { "CustomGame", "BearDeerGame", "BearFoxGame", "BearRacoonGame", "MissileShipGame", "TigerDeerGame", "TigerFoxGame", "TigerRacoonGame" }; // scene names

    public const string BACKGROUND = "BACKGROUND";
    public enum Background { Backyard = 0, Space = 1 }; // dropdown options for background sprites

    public const string PREY = "PREY";
    public enum Prey { Fox = 0, Deer = 1, Pig = 2, Moose = 3, Panda = 4, Rabit = 5, Raccoon = 6, Spaceship = 7}; // dropdown options for prey sprites

    public const string PREDATOR = "PREDATOR";
    public enum Predator { Bear = 0, Tiger = 1, Wolf = 2, Cougar = 3, Missile = 4 }; // dropdown options for predator sprites
    
    // objects that allow user to customize scene
    public GameObject customGameObjects;

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

    [SerializeField]
    private TMP_Dropdown BackgroundDropDown;

    [SerializeField]
    private TMP_Dropdown PreyDropDown;

    [SerializeField]
    private TMP_Dropdown PredatorDropDown;

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

        PlayerPrefs.SetInt(VERSION, (int)Version.Custom);
        this.VersionDropDown.SetValueWithoutNotify((int)(Version.Custom));

        PlayerPrefs.SetInt(BACKGROUND, (int)Background.Backyard);
        this.BackgroundDropDown.SetValueWithoutNotify((int)(Background.Backyard));

        PlayerPrefs.SetInt(PREY, (int)Prey.Fox);
        this.PreyDropDown.SetValueWithoutNotify((int)(Prey.Fox));

        PlayerPrefs.SetInt(PREDATOR, (int)Predator.Bear);
        this.PredatorDropDown.SetValueWithoutNotify((int)(Predator.Bear));
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

    public void SetVersion(int version)
    {
        PlayerPrefs.SetInt(VERSION, version);

        // is this version the custom one
        if (version == (int)Version.Custom)
        {
            customGameObjects.SetActive(true);
            
        }
        else
        {
            customGameObjects.SetActive(false);
        }

    }

    public static string GetVersionName()
    {
        return VersionName[PlayerPrefs.GetInt(VERSION)];
    }

    public void SetBackground(int background)
    {
        PlayerPrefs.SetInt(BACKGROUND, background);
    }

    public void SetPrey(int prey)
    {
        PlayerPrefs.SetInt(PREY, prey);
    }

    public void SetPredator(int predator)
    {
        PlayerPrefs.SetInt(PREDATOR, predator);
    }
}
