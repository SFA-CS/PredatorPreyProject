using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyTurnState : TurnState
{
        
    public PreyTurnState() : base()    {
    }

    
    // called when state is entered
    public override void Enter()
    {
        // show the legal move area for all prey
        this.avatars = GameManager.Instance.Prey; 
        base.Enter();
    }

    
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }

    
    public override void HandleInput(GameObject clickedObject, Vector2 location)
    {
        base.HandleInput(clickedObject, location);       

        if (this.allAvatarInputReceived())
        {
            GameState gameState = new PredatorTurnState();
            GameManager.Instance.SetState(gameState);
        }
    }
}
