// The intended use of this class is to create a 3D surface from a parametization using
// the parameters s and t.
// 

using UnityEngine;
//using UnityEditor;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
//[ExecuteInEditMode] // this should show the object in the scence view
// https://docs.unity3d.com/ScriptReference/ExecuteInEditMode.html
public class SurfaceMesh : MonoBehaviour  {

	
	// the number of segments to produce the grid for the s and t parameters
	private int aSegments, tSegments;

	[SerializeField]
	[Tooltip("The maximum travel distance")]
	private float distance = 2.0f;
	public float MaxTravelDistance { get { return distance; } }


	[SerializeField]
	[Tooltip("The turing radius.")]
	private float radius = 1.4f;
	public float TurningRadius { get { return radius; } }


    private float deltaA = 0.1f; // step size for a parameter

	
    protected float deltaT = 0.1f; // step size in "t" direction.

    
   // vector holding the vertices for the mesh
	protected Vector3[] vertices;
    public Vector3[] Vertices { get { return this.vertices; } }

	// mesh for the mesh filter componenet and will hold the vertices
	protected Mesh mesh;


	//----------------------Coordinate Functions------------------
	private float xcoor(float a, float t) {
        // the x coordinate of a point of the surface under the given parameterization
        // 1/a (1-cos(d*a*t), 
		return 1 / a * (1 - Mathf.Cos(this.distance * a * t));
	}

	private float zcoor(float s, float t) {
        // the z coordinate of a point of the surface under the given parameterization
        // 1/a*sin(d*a*t)
        return 1/aSegments * Mathf.Sin(this.distance*aSegments*t);
	}


	private float ycoor(float s, float t) {
        // the y coordinate of a point of the surface under the given parameterization
        return 0;
	}

    
    //----------------GetVertices--------------------
    public Vector3[] GetVertices()
    {
        return this.vertices; 
    }


    //---------------------SetTriangles--------------------
    private void SetTriangles() {
		// We compute the triangles of the mesh.
		// Two sets of triangles are found: counter clockwise and clockwise
		// This is so the mesh is visible from both "sides" of each triangle

		int numVertices = mesh.vertices.Length; // number of vertices

		int[] counterClockwise = new int[aSegments * tSegments * 6];
		int[] clockwise = new int[aSegments * tSegments * 6];
		// vi is the vertex index
		for (int ti = 0, vi = 0, y = 0; y < tSegments; y++, vi++) {
			for (int x = 0; x < aSegments; x++, ti += 6, vi++) {
				// counter clockwise triangles
				clockwise[ti] = vi;
				clockwise[ti + 3] = clockwise[ti + 2] = vi + 1;
				clockwise[ti + 4] = clockwise[ti + 1] = vi + aSegments + 1;
				clockwise[ti + 5] = vi + aSegments + 2;


				// clockwise triangles
				counterClockwise[ti] = vi+numVertices;
				counterClockwise[ti + 5] = counterClockwise[ti + 2] =  vi + aSegments + 1+numVertices; 
				counterClockwise [ti + 3] = counterClockwise [ti + 1] = vi + 1+numVertices;
				counterClockwise[ti + 4] = vi + aSegments + 2+numVertices;
			}
		}

		// we are double the vertices
		// essentially we have one copy for the clockwise triangles and another for
		// the counter clockwise triangles. We do this so normals are calculated correctly
		// and don't cancel each other out. Normals average triangles connected to them
		Vector3[] doubleVertices = new Vector3[mesh.vertices.Length + mesh.vertices.Length];
		mesh.vertices.CopyTo (doubleVertices, 0);
		mesh.vertices.CopyTo (doubleVertices, mesh.vertices.Length);
		mesh.vertices = doubleVertices;


		// combine the the two sets of triangles and add them to the mesh
		int[] triangles = new int[counterClockwise.Length + clockwise.Length];
		counterClockwise.CopyTo (triangles, 0);
		clockwise.CopyTo (triangles, counterClockwise.Length);
		mesh.triangles = triangles;

		// recalcuate the normals
		mesh.RecalculateNormals ();
	}

	//------------------SetSegments-----------------------------
	// This method sets the number of x and y coordinates needed given
	// the start and end of the interval and setp size. For instance, to go from
	// 1.5 to 2.5 in increments of 0.1, we need 11 and 10 segments points for x.
	private void SetSegments() {
		aSegments = Mathf.CeilToInt ((2/this.radius) / this.deltaA); 
		tSegments = Mathf.CeilToInt (1 / deltaT); 
	}

    //-----------------GetSegments------------------------
    // Returns the number of segments in each direction ("parameter").
    public int GetNumberOfaSegments()
    {
        return this.aSegments;
    }
    public int GetNumberOfTSegments()
    {
        return this.tSegments;
    }

    //-----------------------Generate-------------------------
    // this method generates the mesh for the graph
    void Generate () {
	    GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        //mesh.name = this.surfaceName;
        //this.gameObject.name = this.surfaceName;
        mesh.name = this.gameObject.name;

		// set the number (s,t) coordinates needed to make the mesh
		SetSegments ();


		// the (s,t) value for each point
		float sval = 0;
		float tval = 0;

		// we need one more point than the number of segments
		// for example 10 segments requires 11 points
		this.vertices = new Vector3[(aSegments + 1) * (tSegments + 1)];

		// we also create a uv map to get get better lighting effects on the surface
		Vector2[] uv = new Vector2[vertices.Length];

		for (int i = 0, t = 0; t <= tSegments; t++) {
			for (int a = 0; a <= aSegments; a++, i++) {
				sval = -1/this.radius + a * this.deltaA;
				tval =  t * deltaT;
				// set the coordinate of the vertex
				this.vertices[i] = new Vector3(xcoor(sval, tval),  zcoor(sval,tval), ycoor(sval, tval));


				// set the uv using the position relative to the total size
				uv[i] = new Vector2( (float)a /(float) aSegments, (float)t/(float) tSegments);

			}
		}
		// set the vectices for the mesh
		mesh.vertices = this.vertices;

		// set the triangles of the mesh
		SetTriangles ();

		// set the base textures for the mesh
		// this needs to be the same size as the
		// vertices which has been doubled to provide
		// normals for both sides of the object
		Vector2[] doubleUV = new Vector2[uv.Length + uv.Length];
		uv.CopyTo (doubleUV, 0);
		uv.CopyTo (doubleUV, uv.Length);
		mesh.uv =doubleUV;

	}


    //-----------------------AddCollider---------------------------
    private void SetBoxCollider()
    {
        // when we start add a box collider
        // it will be automatically set the size of the bounding box of the mesh
        if (gameObject.GetComponent<Collider>() == null)
            gameObject.AddComponent<BoxCollider>();
        else
        {
            // if it already has one, get the bounding box to make that of the mesh
            BoxCollider c = gameObject.GetComponent<BoxCollider>();
            c.enabled = true;
            c.center = mesh.bounds.center;
            c.size = mesh.bounds.extents;            
        }
    }

    //-----------------------CreateSurface---------------
    // This method creates the surface and box collider if desired.
    public void CreateSurface()
    {
        this.Generate();
        this.SetBoxCollider();
    }

    

    // ------------------------ Awake ---------------------------------
    // this is called when we enter play mode
    protected void Awake()
	{
        this.CreateSurface();
    }

}
