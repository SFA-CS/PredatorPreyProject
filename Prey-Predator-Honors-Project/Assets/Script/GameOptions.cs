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
    public enum Version { BearDeer = 0, BearFox = 1, BearRacoon = 2, MissileShip = 3, TigerDeer = 4, TigerFox = 5, TigerRacoon = 6, Custom = 7 }; // choices in dropdown
    public static readonly string[] VersionName = { "BearDeerGame", "BearFoxGame", "BearRacoonGame", "MissileShipGame", "TigerDeerGame", "TigerFoxGame", "TigerRacoonGame", "CustomGame" }; // scene names

    public const string BACKGROUND = "BACKGROUND";
    public enum Background { Space = 0, Backyard = 1 };

    public const string PREY = "PREY";
    public enum Prey { Fox = 0, Deer = 1 };

    public const string PREDATOR = "PREDATOR";
    public enum Predator { Bear = 0, Tiger = 1 };
    
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
        Debug.Log("We are here1");

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

        PlayerPrefs.SetInt(BACKGROUND, (int)Background.Space);
        this.BackgroundDropDown.SetValueWithoutNotify((int)(Background.Space));

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
        Debug.Log("We are here Version");

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
        Debug.Log("We are here Background");

        PlayerPrefs.SetInt(BACKGROUND, background);
    }

    public void SetPrey(int prey)
    {
        Debug.Log("We are here Prey");

        PlayerPrefs.SetInt(PREY, prey);
    }

    public void SetPredator(int predator)
    {
        Debug.Log("We are here Predator");

        PlayerPrefs.SetInt(PREDATOR, predator);
    }
}
