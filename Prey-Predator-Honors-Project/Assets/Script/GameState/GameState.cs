using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState 
{
    
    public virtual void Enter() { } 
    public virtual void Exit() { }

    public virtual void Update() { }

    public virtual void HandleInput(GameObject clickedObject, Vector2 location) { }


}
