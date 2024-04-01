using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorTurnState : TurnState
{

  
    public PredatorTurnState()   : base()  {
    }

   
    // called when state is entered
    public override void Enter()
    {
        // show the legal move area for all predators
        this.avatars = GameManager.Instance.Predators;
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
            GameState gameState = new MoveAvatorsState();
            GameManager.Instance.SetState(gameState);
        }
    }
}
