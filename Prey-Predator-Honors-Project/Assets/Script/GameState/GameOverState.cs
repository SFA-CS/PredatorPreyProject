using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : GameState
{
    [SerializeField]
    private GameOverScreen gameOverScreen;

    public GameOverState() : base()
    {

    }

    public override void Enter()
    {
        base.Enter();
        string msg = "Prey Win";
        if (GameManager.Instance.AllPreyCaught())
        {
            msg = "Predators Win";
        }
        this.gameOverScreen.Display(msg);
    }

    public override void Exit() { base.Exit();}

    public virtual void Update() {
        base.Update();
        // nothing to do
    }

    public virtual void HandleInput(GameObject clickedObject, Vector2 location) {
        base.HandleInput(clickedObject, location);
        // nothing to do
    }
}
