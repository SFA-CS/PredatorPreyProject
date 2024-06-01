using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnState : GameState
{

    protected List<Avatar> avatars;
    public TurnState()     {
    }

    
    // called when state is entered
    public override void Enter()
    {
        base.Enter();
      
        foreach (Avatar avatar in avatars)
        {            
            avatar.ShowLegalMoveArea();
        }
    }

    
    public override void Exit()
    {
        base.Exit();     
    }

    public override void Update()
    {
        base.Update();
    }

    // checks to see if all avatars have been selected for move
    protected bool allAvatarInputReceived()
    {
        foreach (Avatar avatar in avatars)
        {
         
            if (!avatar.DestinationSelected())
                return false;
        }
        return true; 
    }
    public override void HandleInput(GameObject clickedObject, Vector2 location)
    {
        // if we clicked on mesh for prey
        foreach (Avatar avatar in avatars)
        {            
            if (avatar.MovementArea.gameObject == clickedObject)
            {
                Debug.Log("Handle Input: Local location of click: " + avatar.gameObject.name + " " + avatar.transform.InverseTransformPoint(location));
                //Debug.Log(avatar.transform.InverseTransformDirection(location));
                avatar.HideLegalMoveArea();
                //avatar.Destination = location;              
                avatar.Destination = avatar.transform.InverseTransformPoint(location);
            }
        }       
        
    }
}
