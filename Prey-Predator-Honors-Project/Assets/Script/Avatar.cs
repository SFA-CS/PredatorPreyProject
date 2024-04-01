using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{

    [SerializeField]
    [Tooltip("The maximum travel distance")]
    private float distance = 2.0f;
    public float MaxTravelDistance { get { return distance; } }

    [SerializeField]
    [Tooltip("The turing radius.")]
    private float radius = 1.4f;
    public float TurningRadius { get { return radius; } }

    [SerializeField]
    [Tooltip("Create legal move area for predator/prey.")]
    private MoveArea moveArea;
    public MoveArea MovementArea { get { return moveArea; } }

    private Vector2 destination;
    public Vector2 Destination {  get { return destination; } set { destination = value; } }
        

    public void Start()
    {
        this.moveArea.CreateMoveArea(this.radius, this.distance);
    }

    public void HideLegalMoveArea()
    {
        this.moveArea.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    public void ShowLegalMoveArea()
    {
        this.moveArea.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    public bool DestinationSelected()
    {
        return !this.moveArea.gameObject.GetComponent<MeshRenderer>().enabled;
    }
}
