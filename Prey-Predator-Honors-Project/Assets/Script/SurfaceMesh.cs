// The intended use of this class is to create a 3D surface from a parametization using
// the parameters s and t.
// 
using org.mariuszgromada.math.mxparser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
//[ExecuteInEditMode] // this should show the object in the scence view
// https://docs.unity3d.com/ScriptReference/ExecuteInEditMode.html
public class SurfaceMesh : InteractionObject  {

	// define an event for  object is redrawn
	public delegate void RedrawnEvent();
	public event RedrawnEvent Redrawn;


   // [SerializeField]
   // [Tooltip("Object to draw size relative to.")]
   // private GameObject TransformObject; 

	// the number of segments to produce the grid for the s and t parameters
	private int sSegments, tSegments; 

	//[SerializeField]
	//[Tooltip("The name of the surface. The game object will also carry this name.")]
	//public string surfaceName = "Surface";

	[SerializeField]	
	[Tooltip("The beggining of the s interval.")]
	protected float startS = -2.5f; // start of "s" interval

    [SerializeField]
	[Tooltip("The end of the s interval.")]
	protected float endS = 2.5f;    // end of "s" interval
	[SerializeField]
	[Tooltip("The step size in the s direction.")]
	protected float deltaS = 0.1f; // step size in "s" direction

	[SerializeField]	
	[Tooltip("The beggining of the t interval.")]
	protected float startT = -2.5f; // start of "t" interval
	[SerializeField]	
	[Tooltip("The beggining of the t interval.")]
	protected float endT = 2.5f; // end of the "t" interval
	[SerializeField]
	[Tooltip("The step size in the t direction.")]
	protected float deltaT = 0.1f; // step size in "t" direction.

	[SerializeField]
	[Tooltip("The function for the x coordinate of the surface in terms of s and t.")]
	protected string x = "s";
	[SerializeField]
	[Tooltip("The function for the y coordinate of the surface in terms of s and t.")]
	protected string y = "t";
	[SerializeField]
	[Tooltip("The function for the z coordinate of the surface in terms of s and t.")]
	protected string z = "s*t";

    [SerializeField]
    [Tooltip("Add a box collider to the object.")]
    private bool addCollider = false;

	// used in desgin phase to redraw the object with restarting the scene
	public bool redrawSurface = false;

   
	// function created from the strings given
	private Function xfunc;
	private Function yfunc;
	private Function zfunc;

	// vector holding the vertices for the mesh
	protected Vector3[] vertices;
    public Vector3[] Vertices { get { return this.vertices; } }

	// mesh for the mesh filter componenet and will hold the vertices
	protected Mesh mesh;


	//----------------------Coordinate Functions------------------
	private float xcoor(float s, float t) {
		// the x coordinate of a point of the surface under the given parameterization
		return (float) xfunc.calculate(s,t);

	}

	private float zcoor(float s, float t) {
        // the z coordinate of a point of the surface under the given parameterization
        return (float) zfunc.calculate(s,t);

	}


	private float ycoor(float s, float t) {
		// the y coordinate of a point of the surface under the given parameterization
		return (float) yfunc.calculate(s,t);

	}

    //----------------------GetPointAt--------------------
    // This returns the point on surface mesh with the given value of s,t
    // NOTE: functions are not updated until after Update method runs
    public Vector3 GetPointAt(float s, float t)
    {
        return new Vector3(xcoor(s, t), ycoor(s, t), zcoor(s, t));
    }

    // Same as above, but returns a Unity position
    public Vector3 GetPositionAt(float s, float t)
    {

        return new Vector3(xcoor(s, t),  zcoor(s, t), ycoor(s, t));
    }

    //-------------------SetFunctions--------------------
    public void SetX(string newx)
    {        
        this.x = newx;
        this.xfunc = new Function("x(s,t) = " + x);
    }

    public void SetY(string newy)
    {
        this.y = newy;
        this.yfunc = new Function("y(s,t) = " + y);
    }

    public void SetZ(string newz)
    {
        this.z = newz;
        this.zfunc = new Function("z(s,t) = " + z);
    }

    public void SetFunctions(string newx, string newy, string newz)
    {
        this.SetX(newx);
        this.SetY(newy);
        this.SetZ(newz);
    }

    //----------------SetIntervals--------------------
    public void SetS(float start, float end, float delta)
    {
        this.startS = start;
        this.endS = end;
        this.deltaS = delta;
    }

    public void SetT(float start, float end, float delta)
    {
        this.startT = start;
        this.endT = end;
        this.deltaT = delta;
    }

    //-----------------GetS, T------------------------
    public float GetStartS() { return this.startS; }
    public float GetEndS() { return this.endS;  }
    public float GetDeltaS() { return this.deltaS;  }
    public Vector3 GetSProperties() { return new Vector3(this.startS, this.endS, this.deltaS); }
    public float GetStartT() { return this.startT; }
    public float GetEndT() { return this.endT; }
    public float GetDeltaT() { return this.deltaT; }
    public Vector3 GetTProperties() { return new Vector3(this.startT, this.endT, this.deltaT); }


    //----------------GetVertices--------------------
    public Vector3[] GetVertices()
    {
        return this.vertices; 
    }
    //--------------------Redraw------------------------
    public void Redraw()
    // this method is called to redraw the function when desired
    {

        this.redrawSurface= true;
		if (Redrawn != null) 
			Redrawn ();
    }


    //---------------------SetTriangles--------------------
    private void SetTriangles() {
		// We compute the triangles of the mesh.
		// Two sets of triangles are found: counter clockwise and clockwise
		// This is so the mesh is visible from both "sides" of each triangle

		int numVertices = mesh.vertices.Length; // number of vertices

		int[] counterClockwise = new int[sSegments * tSegments * 6];
		int[] clockwise = new int[sSegments * tSegments * 6];
		// vi is the vertex index
		for (int ti = 0, vi = 0, y = 0; y < tSegments; y++, vi++) {
			for (int x = 0; x < sSegments; x++, ti += 6, vi++) {
				// counter clockwise triangles
				clockwise[ti] = vi;
				clockwise[ti + 3] = clockwise[ti + 2] = vi + 1;
				clockwise[ti + 4] = clockwise[ti + 1] = vi + sSegments + 1;
				clockwise[ti + 5] = vi + sSegments + 2;


				// clockwise triangles
				counterClockwise[ti] = vi+numVertices;
				counterClockwise[ti + 5] = counterClockwise[ti + 2] =  vi + sSegments + 1+numVertices; 
				counterClockwise [ti + 3] = counterClockwise [ti + 1] = vi + 1+numVertices;
				counterClockwise[ti + 4] = vi + sSegments + 2+numVertices;
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
		sSegments = Mathf.CeilToInt ((endS - startS) / deltaS); 
		tSegments = Mathf.CeilToInt ((endT - startT) / deltaT); 
	}

    //-----------------GetSegments------------------------
    // Returns the number of segments in each direction ("parameter").
    public int GetNumberOfSSegments()
    {
        return this.sSegments;
    }
    public int GetNumberOfTSegments()
    {
        return this.tSegments;
    }

    //-----------------------Generate-------------------------
    // this method generates the mesh for the graph
    void Generate () {
		// create the functions needed
		this.xfunc = new Function("x(s,t) = " + x);
		this.yfunc = new Function("y(s,t) = " + y);
		this.zfunc = new Function ("z(s,t) = " + z); 

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
		this.vertices = new Vector3[(sSegments + 1) * (tSegments + 1)];

		// we also create a uv map to get get better lighting effects on the surface
		Vector2[] uv = new Vector2[vertices.Length];

		for (int i = 0, t = 0; t <= tSegments; t++) {
			for (int s = 0; s <= sSegments; s++, i++) {
				sval = startS + s * deltaS;
				tval = startT + t * deltaT;
				// set the coordinate of the vertex
				this.vertices[i] = new Vector3(xcoor(sval, tval),  zcoor(sval,tval), ycoor(sval, tval));


				// set the uv using the position relative to the total size
				uv[i] = new Vector2( (float)s/(float) sSegments, (float)t/(float) tSegments);

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
        if (this.addCollider)
            this.SetBoxCollider();
        else
        {
            if (gameObject.GetComponent<Collider>() != null)
                gameObject.GetComponent<Collider>().enabled = false;
            // disable the box collider if it exists
        }

        
    }

    //-----------------partialFirstParameterUnityCoordinates--------------
    public Vector3 partialFirstParameterUnityCoordinates(float value, float fixedVal)
    {
        Argument s = new Argument("s" , value);
        Argument t = new Argument("t", fixedVal);

        Function x1 = new Function("x1(s,t) = der(" + this.x + ", s)");
        Function y1 = new Function("y1(s,t) = der(" + this.y + ", s)");
        Function z1 = new Function("z1(s,t) = der(" + this.z + ", s)");

        

        return new Vector3((float)x1.calculate(s, t), (float)z1.calculate(s, t), (float)y1.calculate(s, t));
    }

    
    //-----------------partialSecondParameterUnityCoordinates--------------
    // The value and fixedVal need to be set properly to the usage of this method (maybe switch these????)
    public Vector3 partialSecondParameterUnityCoordinates(float value, float fixedVal)
    {
        Argument s = new Argument("s" , fixedVal);
        Argument t = new Argument("t", value);

        Function x2 = new Function("x2(s,t) = der(" + this.x + ", t)");
        Function y2 = new Function("y2(s,t) = der(" + this.y + ", t)");
        Function z2 = new Function("z2(s,t) = der(" + this.z + ", t)");

        return new Vector3((float)x2.calculate(s, t), (float)z2.calculate(s, t), (float)y2.calculate(s, t));
    }



    //-------------------PartialX------------------
    // This method returns the first partial w.r.t x for a function in the form z = f(x,y) at the point x0, y0
    public float partialX(float x0, float y0)
    {
        Argument x = new Argument("s", x0);
        Argument y = new Argument("t", y0);

        Function fx = new Function("z1(s,t) = der(" + this.z + ", s)");

        return (float)fx.calculate(x, y);
    }

    //-------------------PartialY------------------
    // This method returns the first partial w.r.t. y for a function in the form z = f(x,y) at the point x0, y0
    public float partialY(float x0, float y0)
    {
        Argument x = new Argument("s", x0);
        Argument y = new Argument("t", y0);

        Function fx = new Function("z1(s,t) = der(" + this.z + ", t)");

        return (float)fx.calculate(x, y);
    }

    // ------------------------ Awake ---------------------------------
    // this is called when we enter play mode
    protected void Awake()
	{
        //if (this.TransformObject != null)
        //    this.transform.parent = this.TransformObject.transform;
        // when we enter play mode, we generate the points and show the surface
        Debug.Log("Surface Mesh Awake - Generating Surface given by " + this.x + "   " + this.y + "   " + this.z);
        this.CreateSurface();
    }
    // --------------------------------------------------------
    // The is enteded to only be used during the editing phase of the scene
    // so the creator can redraw the surface "on the fly". 

    protected virtual void Update() {
		// redraw the surface only when necessary
		if (redrawSurface) {
            this.CreateSurface();
			redrawSurface = false; // set to false, so we only redraw once
		}
	}

    public Vector3 NormalVectorfromParam(float t0, float s0)
    {
        return -Vector3.Cross(partialFirstParameterUnityCoordinates(s0, t0), partialSecondParameterUnityCoordinates(t0, s0));
    }
}
