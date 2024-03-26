using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MoveAvatorsState : GameState 
{
    private List<Avatar> avatars;
    
    public MoveAvatorsState() : base()
    {
        avatars = new List<Avatar>(GameManager.Instance.Prey);
        avatars.AddRange(GameManager.Instance.Predators);        
    }

    public override void Enter()
    {
        base.Enter();
    }

    private bool allReachedDestination()
    {
        foreach (Avatar avatar in avatars)
        {
            if (Vector3.Distance(avatar.transform.localPosition, avatar.Destination) >= 0.01)
            {
                return false;
            }
        }
        return true;
    }

    public override void Exit() { base.Exit();
        // switch to other turn
    }
    public override void Update() {
        foreach (Avatar avatar in this.avatars)
        {
            Vector3 destination = new Vector3(avatar.Destination.x, avatar.Destination.y, 0);
            avatar.transform.localPosition = Vector3.MoveTowards(avatar.transform.localPosition, avatar.Destination, 10 * Time.deltaTime);
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
