using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacterState : GameState 
{
    private  Avatar avatar; // avatar to move
    private Vector2 location; // location to move to
    public MoveCharacterState(Avatar avatar, Vector2 moveToPosition) 
    {
        this.avatar = avatar;
        this.location = moveToPosition;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit() { base.Exit();
        // switch to other turn
    }

    public override void Update() { base.Update();
        // gradually move avatar to desired location
    }

    public override void HandleInput(GameObject clickedObject, Vector2 location)
    {
        //  ignore input while moving
    }
}
