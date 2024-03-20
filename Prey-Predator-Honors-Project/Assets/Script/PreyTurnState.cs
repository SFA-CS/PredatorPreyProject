using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyTurnState : GameState
{

    private List<Avatar> avatars;
    public PreyTurnState()     {
    }

    
    
    public override void Enter()
    {
        this.avatars = GameManager.Instance.Prey; 
        foreach (Avatar avatar in avatars)
        {
            avatar.ShowLegalMoveArea();
        }
    }

    public override void Exit()
    {
        foreach (Avatar avatar in avatars)
        {
            avatar.HideLegalMoveArea();
        }
        
    }

    public override void Update()
    {
        // nothing to do
    }


    public override void HandleInput(GameObject clickedObject, Vector2 location)
    {
        // if we clicked on mesh for prey
        foreach (Avatar avatar in avatars)
        {
            
            if (avatar.MovementArea.gameObject == clickedObject)
            {
                MoveCharacterState moveState = new MoveCharacterState(avatar, location);
                GameManager.Instance.SetState(moveState);
            }
        }

        
    }
}
