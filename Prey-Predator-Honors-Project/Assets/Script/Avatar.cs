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
    public float MaxTravelDistance { get { return distance; }  }

    [SerializeField]
    [Tooltip("The turing radius.")]
    private float radius = 1.4f;
    public float TurningRadius
    {
        get { return radius; }       
    }

        [SerializeField]
    [Tooltip("Create legal move area for predator/prey.")]
    private MoveArea moveArea;
    public MoveArea MovementArea { get { return moveArea; }  }

    private Vector2 destination;
    public Vector2 Destination {  get { return destination; } set { destination = value; } }

    // path the avatar will follow when it moves
    private Vector2[] path;
    private Vector2[] rotation;
    private const int PATH_LENGTH = 25;
    private int pathIndex = 0;
    private GameObject copy; // for transform

    public void CreateMovementArea(float turnRadius, float maxTravelDistance)
    {
        if (maxTravelDistance > 0)
            this.distance = maxTravelDistance;
        if (turnRadius > 0)  
            this.radius = turnRadius;
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
        this.rotation = new Vector2[PATH_LENGTH];
        this.pathIndex = 0; 

        Vector2 position = new Vector2(this.transform.position.x, this.transform.position.y); 
        Vector2 localDestination = this.destination;
        Debug.Log("position " + position + " localDestination " + localDestination + " Destination " + this.destination);
        //  parameter for path to follow
        float u =  2.0f * localDestination.x/ localDestination.sqrMagnitude;
        float endT = localDestination.sqrMagnitude * Mathf.Atan(localDestination.x / localDestination.y) / (this.MaxTravelDistance * localDestination.x);
        
        // here is how we solved for the above
        // https://www.wolframalpha.com/input?i=solve+for+u+and+v+%7Bx+%3D+1%2Fu*%281-cos%28d*u*v%29%29%2C+y+%3D+1%2Fu*sin%28d*u*v%29+%7D
        float x = 0.0f;
        float y = 0.0f;
        float t = 0.0f; // paramter        

        // create the path (that is set the specified number of points to move along the path        
        int count = 0;
        do
        {
            // compute point along path
            t = Mathf.Lerp(0.0f, endT, (count+1) / 25.0f);
            x = 1 / u * (1 - Mathf.Cos(this.distance * u * t));
            y = 1 / u * Mathf.Sin(this.distance * u * t);            
            this.path[count] = new Vector2(x, y); ;

            // compute rotation vector
            x = Mathf.Sin(this.distance * u * t);
            y = Mathf.Cos(this.distance * u * t);
            this.rotation[count] = new Vector2(x, y);
            count++;            
        } while (count < PATH_LENGTH);

        Debug.Log(this.gameObject.name);
        Debug.Log("endT " + endT + " t " + t + " count " +  count + " u " + u + " last point in path " + this.path[path.Length-1] + " localDesitnation " + localDestination);
        Debug.Log("End Tangent Vector: " + x + ", " + y);
        
        // keep a copy of the current transform for reference as we move along path
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
            // set position
            Vector3 newPostion = this.copy.transform.TransformPoint(this.path[pathIndex]);
            //Vector3 diff = newPostion - this.transform.localPosition;
            this.transform.localPosition = newPostion;
            
            
            // set rotation by finding angle with z-axis
            // this will be in radians
            float angle = Mathf.Acos(rotation[pathIndex].y); // angle with <0,1>
            if (rotation[pathIndex].x > 0)
            {
                angle = -angle;
            }
            // change to degrees
            angle = angle * Mathf.Rad2Deg;
            if (pathIndex == PATH_LENGTH - 1)
            {
                Debug.Log(this.gameObject.name + " angle: " + angle);
            }            
            this.transform.localEulerAngles = this.copy.transform.localEulerAngles + new Vector3(0,0,angle);
            
            // move to next point in the path
            this.pathIndex++;
        }
    }

    

    public bool ReachedDestination()
    {
        return (this.pathIndex >= PATH_LENGTH);
    }
}
