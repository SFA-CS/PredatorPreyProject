using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MoveAvatorsState : GameState 
{
    private List<Avatar> avatars;
    private float elaspsedTime = 0.0f;
   
    public MoveAvatorsState() : base()
    {
        // we move the predators and prey simultaneously
        avatars = new List<Avatar>(GameManager.Instance.Prey);
        avatars.AddRange(GameManager.Instance.Predators);        
    }

    public override void Enter()
    {
        base.Enter();
        foreach (Avatar avatar in avatars)
        {
            avatar.ComputePath();
        }
    }

    private bool allReachedDestination()
    {
        foreach (Avatar avatar in avatars)
        {
            if (!avatar.ReachedDestination())
            {
                return false;
            }
        }
        return true;
    }

    public override void Exit() { 
        base.Exit();
        Scoreboard.Instance.NumTurns = Scoreboard.Instance.NumTurns + 1;
        if (Scoreboard.Instance.NumTurns == Scoreboard.Instance.MaxTurns)
        {
            GameOverState gameOverState = new GameOverState();
            GameManager.Instance.SetState(gameOverState);
        }
    }
    public override void Update() {
        // move each predator/prey one step (one point on the path at a time)
        foreach (Avatar avatar in this.avatars)
        {
            
            elaspsedTime += Time.deltaTime;
            if (elaspsedTime >= 0.1f)
            {
                avatar.MoveAlongPath();
                elaspsedTime = 0.0f;
            }
        }   
        
        if (this.allReachedDestination())
        {
            PreyTurnState preyTurn = new PreyTurnState();
            GameManager.Instance.SetState(preyTurn);
        }
    }

    public override void HandleInput(GameObject clickedObject, Vector2 location)
    {
        //  ignore input while moving
    }
}
