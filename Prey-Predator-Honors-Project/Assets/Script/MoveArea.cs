// The intended use of this class is to create the legal move area based on distance traveled and turning radius.
// Authors: JJB

using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(PolygonCollider2D))]
public class MoveArea : MonoBehaviour  {
	
	[SerializeField]
	[Tooltip("The maximum travel distance")]
	private float distance = 2.0f;
	public float MaxTravelDistance { get { return distance; } }

	[SerializeField]
	[Tooltip("The turing radius.")]
	private float radius = 1.4f;
	public float TurningRadius { get { return radius; } }

	[SerializeField]
	[Tooltip("The number of points to use in the parameterization of the mesh.")]
	private int numPoints = 10; // number of points to use for each parameter when creating the mesh

	// mesh for the mesh filter componenet and will hold the vertices
	private Mesh mesh;

	//----------------------Coordinate Functions------------------
	private float xcoor(float a, float t) {        
        // 1/a (1-cos(d*a*t), 
		return 1 / a * (1 - Mathf.Cos(this.distance * a * t));
	}

	private float ycoor(float a, float t) {        
        // 1/a*sin(d*a*t)
        return 1/a* Mathf.Sin(this.distance*a*t);
	}

	private float zcoor(float a, float t) {        
        return 0;
	}
   
    //---------------------SetTriangles--------------------
    private void SetTriangles() {
		// We compute the triangles of the mesh.
		// Two sets of triangles are found: counter clockwise and clockwise
		// This is so the mesh is visible from both "sides" of each triangle

		int numVertices = mesh.vertices.Length; // number of vertices

		int aSegments = this.numPoints - 1; // 11 points make 10 segments
		int tSegments = this.numPoints - 1; 

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

		// We are doubling the vertices
		// We have one copy for the clockwise triangles and another for
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

    //-----------------------Generate-------------------------
    // this method generates the mesh for the graph
	private void Generate () {
	    GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		
        mesh.name = this.gameObject.name;

		// set the number (a,t) coordinates needed to make the mesh
		//SetSegments ();

		// the (a,t) value for each point
		float aval = 0;
		float tval = 0;

		// we need one more point than the number of segments
		// for example 10 segments requires 11 points
		Vector3[] vertices = new Vector3[this.numPoints*this.numPoints];

		// we also create a uv map to get get better lighting effects on the surface
		Vector2[] uv = new Vector2[vertices.Length];

		float delta = 1.0f / (this.numPoints-1);
		for (int i = 0, t = 0; t <  this.numPoints; t++) {
			for (int a = 0; a < this.numPoints; a++, i++) {
				aval = -1.0f/this.radius * (1-a*delta) + 1.0f/this.radius * a * delta;  // goes from -1/R to 1/R
				tval =  t * delta; // goes from 0 to 1
				// set the coordinate of the vertex
				vertices[i] = new Vector3(xcoor(aval, tval),  ycoor(aval,tval), zcoor(aval, tval));

				// set the uv using the position relative to the total size
				uv[i] = new Vector2((float)a / (float)(this.numPoints - 1), (float)t / (float)(this.numPoints - 1));
			}
		}
		// set the vectices for the mesh
		mesh.vertices = vertices;

		// set the triangles of the mesh
		this.SetTriangles ();

		// set the base textures for the mesh
		// this needs to be the same size as the
		// vertices which has been doubled to provide
		// normals for both sides of the object
		Vector2[] doubleUV = new Vector2[uv.Length + uv.Length];
		uv.CopyTo (doubleUV, 0);
		uv.CopyTo (doubleUV, uv.Length);
		mesh.uv =doubleUV;    
		
    }

	private void DefineCollider()
	{
		PolygonCollider2D collider = this.GetComponent<PolygonCollider2D>();

		// 3 sides, left, top, right
		Vector2[] outline = new Vector2[3*this.numPoints];

		// left side boundary of shape bottom to top
		float tval = 0;
		float aval = -1.0f / radius; // constant on right side
		float delta = 1.0f / (this.numPoints-1); 
		for (int i = 0; i < this.numPoints; i++)
		{
			tval = i * delta;  // goes 0 to 1
			outline[i] = new Vector2(this.xcoor(aval, tval), this.ycoor(aval, tval));
		}

		// top of shape, left to right
		tval = 1;
		for (int i = 0;  i< this.numPoints; i++)
		{
            aval = -1.0f / this.radius * (1- i * delta) + 1.0f/this.radius * (i*delta);
            outline[i+this.numPoints] = new Vector2(this.xcoor(aval, tval), this.ycoor(aval, tval));
        }

        // right of shape, top to bottom        
		aval = 1.0f/ radius;
        for (int i = 0; i < this.numPoints; i++)
        {
            tval = 1 - i * delta; // top (t=1) to bottom (t = 0)
            outline[this.numPoints * 2+i] = new Vector2(this.xcoor(aval, tval), this.ycoor(aval, tval));
        }

		collider.SetPath(0, outline);
		collider.enabled = true;
    }

    public void CreateMoveArea(float turningRadius, float maxTravelDistance)
	{
		this.radius = turningRadius;
		this.distance = maxTravelDistance;
		this.Generate();
		this.DefineCollider();
	}

}
