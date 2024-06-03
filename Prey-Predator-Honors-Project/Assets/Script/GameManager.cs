using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    
    [SerializeField]
    [Tooltip("Predator for the game")]
    private List<Avatar> prey;
    public List<Avatar> Prey {  get { return prey; } }

    [SerializeField]
    [Tooltip("Predator for the game")]
    private List<Avatar> predators;
    public List<Avatar> Predators { get { return predators; } }

    private GameState gameState; // holds state of game (State Design Pattern)
    
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // singleton design pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
       
        
    }

    private void Start()
    {
        
        // set up the prey
        int num = PlayerPrefs.GetInt(GameOptions.PREY_NUMBER);
        this.prey.RemoveRange(num, this.prey.Count - num);
        float travelDistance = PlayerPrefs.GetFloat(GameOptions.PREY_DISTANCE);
        float turnRadius = PlayerPrefs.GetFloat(GameOptions.PREY_RADIUS);
        foreach (Avatar avatar in this.prey)
        {
            avatar.gameObject.SetActive(true);
            avatar.CreateMovementArea(turnRadius, travelDistance);            
        }

        // set up the predators
        num = PlayerPrefs.GetInt(GameOptions.PREDATOR_NUMBER);
        this.predators.RemoveRange(num, this.predators.Count - num);
        travelDistance = PlayerPrefs.GetFloat(GameOptions.PREDATOR_DISTANCE);
        turnRadius = PlayerPrefs.GetFloat(GameOptions.PREDATOR_RADIUS);
        foreach (Avatar avatar in this.predators)
        {
            avatar.gameObject.SetActive(true);
            avatar.CreateMovementArea(turnRadius, travelDistance);
            avatar.HideLegalMoveArea();
        }

        // set up location of predator/prey based on proximity
        // Close: 4 x max travel distance
        // Mid : 8 x max travel distance
        // Far: 12 x max travel distance
        int prox = PlayerPrefs.GetInt(GameOptions.PROXIMITY);
        float maxTravelDist = Mathf.Max(this.prey[0].MaxTravelDistance, this.predators[0].MaxTravelDistance);
        float location = ((prox + 1) * 4 * maxTravelDist)/2;
        Debug.Log("Location: " + location + " Prox: " + prox);
        float offset = 0; int count = 0;
        foreach (Avatar avatar in this.prey)
        {
            avatar.gameObject.transform.localPosition = new Vector3(-(location+offset), avatar.transform.localPosition.y, avatar.transform.localPosition.z);
            count++;
            if (count % 3 == 0)
                offset += 2.0f;
        }

        offset = 0; count = 0;
        foreach (Avatar avatar in this.predators)
        {
            avatar.gameObject.transform.localPosition = new Vector3(location + offset, avatar.transform.localPosition.y, avatar.transform.localPosition.z);
            count++;
            if (count % 3 == 0)
                offset += 2.0f;
        }

        // zoom out if necessary
        if (location >= 10)
            Camera.main.orthographicSize = location / 2.0f;

        // set up the scoreboard
        Scoreboard.Instance.MaxTurns = PlayerPrefs.GetInt(GameOptions.TURNS);        
        Scoreboard.Instance.PreyRemaining = this.prey.Count;
        Scoreboard.Instance.NumTurns = 0;

        // set the game state
        this.SetState(new PreyTurnState());
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)  
        {
            // ray cast to determine where the click happened
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray);
            if (rayHit.collider != null)
            {
                //Debug.Log(rayHit.collider.gameObject.name + " positions " + rayHit.collider.gameObject.transform.position);
                this.gameState.HandleInput(rayHit.collider.gameObject, ray.origin);
            }
            Debug.Log("Mouse Position : " + mousePosition + " Ray Origin (Unity Coordinates) " + ray.origin);
            
        }
    }

    public void PreyCaught(GameObject preyCaptured)
    {
        // when a prey is capture, remove it
        // from the game (not visible and not in current list)
        preyCaptured.gameObject.SetActive(false);
        this.prey.Remove(preyCaptured.GetComponent<Avatar>());

        // if all the prey are caught move to the GAmeOverState
        Scoreboard.Instance.PreyRemaining = this.prey.Count; 
        if (this.AllPreyCaught())
        {
            GameOverState gameState = new GameOverState();
            this.SetState(gameState);
        }

    }

    public bool AllPreyCaught()
    {
        return this.prey.Count == 0;
    }
   
    public void Update()
    {
        this.gameState.Update();
       
     }

    public void SetState(GameState newState)
    {
        if (this.gameState != null)
            this.gameState.Exit();

        this.gameState = newState;
        
        this.gameState.Enter();
    }

}
