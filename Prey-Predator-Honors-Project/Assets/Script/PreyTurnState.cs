using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyTurnState : GameState
{

    private List<GameObject> avatars;
    public PreyTurnState()     {
    }

    public void SetAvatars(List<GameObject> avatars)
    {
        this.avatars = avatars;
    }
    
    public override void Enter()
    {
        foreach (GameObject avatar in avatars)
        {
            // show the mesh
        }
    }

    public override void Exit()
    {
        foreach (GameObject avatar in avatars)
        {
            // remove the mesh
        }
        
    }

    public override void Update()
    {
        // nothing to do
    }


    public override void HandleInput(GameObject clickedObject, Vector2 location)
    {
        // if we clicked on mesh for prey
        MoveCharacterState moveState = new MoveCharacterState(clickedObject);
        GameManager.Instance.SetState(moveState);
    }
}
