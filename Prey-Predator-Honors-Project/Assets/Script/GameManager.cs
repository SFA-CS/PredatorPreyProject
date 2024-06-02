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
       
        this.SetState(new PreyTurnState());
    }

    private void Start()
    {
        foreach (Avatar avatar in this.predators)
        {
            avatar.HideLegalMoveArea();
        }

        // TODO: get from options
        Scoreboard.Instance.MaxTurns = 5;
        Scoreboard.Instance.PreyRemaining = this.prey.Count;
        Scoreboard.Instance.NumTurns = 0;
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
