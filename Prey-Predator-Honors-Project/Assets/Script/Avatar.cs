using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
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

    // path the avatar will follow when it moves
    private Vector2[] path;
    private const int PATH_LENGTH = 25;
    private int pathIndex = 0;
    private GameObject copy; // for transform

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

    public void ComputePath()
    {
        this.path = new Vector2[PATH_LENGTH];
        this.pathIndex = 0; 

        Vector2 position = new Vector2(this.transform.position.x, this.transform.position.y); 
        Vector2 localDestination = this.destination;
        Debug.Log("position " + position + " localDestination " + localDestination + " Destination " + this.destination);
        //  parameter for path to follow
        float u =  2.0f * localDestination.x/ localDestination.sqrMagnitude;
        float endT = localDestination.sqrMagnitude * Mathf.Atan(localDestination.x / localDestination.y) / (this.MaxTravelDistance * localDestination.x);
        

        // https://www.wolframalpha.com/input?i=solve+for+u+and+v+%7Bx+%3D+1%2Fu*%281-cos%28d*u*v%29%29%2C+y+%3D+1%2Fu*sin%28d*u*v%29+%7D
        float x = 0.0f;
        float y = 0.0f;
        float t = 0.0f; // paramter        
        Vector2 offset;
        int count = 0;
        do
        {
            t = Mathf.Lerp(0.0f, endT, (count+1) / 25.0f);
            x = 1 / u * (1 - Mathf.Cos(this.distance * u * t));
            y = 1 / u * Mathf.Sin(this.distance * u * t);
            offset = new Vector2(x, y);
            this.path[count] = offset;
            //this.path.Add(offset + position);
            count++;
            // Debug.Log("u " + u  + " x " + x + " y " + y + " offset " + offset + " localDesitnation " + localDestination);
        } while (count < PATH_LENGTH);
        //while ((offset - localDestination).magnitude > 0.001 && count <= 100);
        Debug.Log("endT " + endT + " t " + t + " count " +  count + " u " + u + " x " + x + " y " + y + " offset " + offset + " localDesitnation " + localDestination);
        Debug.Log("Offset Avatar Point: " + this.transform.TransformPoint(offset));
        //Debug.Log("Offset Avatar Direction: " + this.transform.TransformDirection(offset ));
        //Debug.Log("Offset MoveArea Point: " + this.moveArea.gameObject.transform.TransformPoint(offset));
        //Debug.Log("Offset MoveArea Direction: " + this.moveArea.gameObject.transform.TransformDirection(offset));
        //Debug.Log("Offset Parent Point: " + this.gameObject.transform.parent.transform.TransformPoint(offset));
        //Debug.Log("Offset Parent Direction: " + this.gameObject.transform.parent.transform.TransformDirection(offset));
        if (this.copy != null)
        {
            Destroy(this.copy);
        }
        this.copy = Instantiate(this.gameObject, this.transform.parent);
        this.copy.SetActive(false);
        
    }

    public void MoveAlongPath()
    {
        if (!this.ReachedDestination())
        {
            //Debug.Log("Move to " + this.path[pathIndex] + " transform " + this.transform.TransformPoint(this.path[pathIndex]));
            Vector3 newPostion = this.copy.transform.TransformPoint(this.path[pathIndex]);
            Vector3 diff = newPostion - this.transform.localPosition;
            if (pathIndex >= 1)
            {
                Vector2 diffLocal = this.path[pathIndex] - this.path[pathIndex - 1];
                diffLocal = this.copy.transform.InverseTransformDirection(diffLocal);
                float angle = Mathf.Acos(diffLocal.y / diffLocal.magnitude); // angle with <0,1>
                this.transform.Rotate(0, 0, angle);
            }
            this.transform.localPosition = newPostion;
            this.pathIndex++;
        }
    }

    

    public bool ReachedDestination()
    {
        return (this.pathIndex >= PATH_LENGTH);
    }
}
