using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacterState : GameState 
{
    public GameObject avatar; // avatar to move
    public MoveCharacterState(GameObject avatar) 
    {
        this.avatar = avatar;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit() { base.Exit();}

    public override void Update() { base.Update(); }

    public override void HandleInput(GameObject clickedObject, Vector2 location)
    {
        //  ignore input while moving
    }
}
