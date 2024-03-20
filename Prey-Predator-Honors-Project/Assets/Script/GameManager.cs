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

    private GameState gameState;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // singleton design pattern
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        this.SetState(new PreyTurnState());
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.canceled)  // button released
        {
            // ray cast to determine where the click happened
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray);
            if (rayHit.collider != null)
            {
                Debug.Log(rayHit.collider.gameObject.name);
                this.gameState.HandleInput(rayHit.collider.gameObject, mousePosition);
            }
            Debug.Log("Mouse Position : " + mousePosition + " Ray Origin (Unity Coordinates) " + ray.origin);
            
        }
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
        
        if (this.gameState != null)
            this.gameState.Enter();
    }

}
