using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Multiplier. Should not be 0.0")]
    private float movementSpeed = 1.0f;


    private const float MinZoom = 4.0f;
    
    // records the input movement direction 
    private Vector2 movementDirection = Vector2.zero;

    // bools to determine when the user is clicking/letting go of ZoomIn, ZoomOut buttons
    private bool zoomIn = false;
    private bool zoomOut = false;
    private float orthoSize = 7.5f; // stores default size for camera; used to reset zooming


    public static CameraController Instance { get; private set; }

    private void Awake()
    {
        // singleton design pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }



    private void Start()
    {
        this.orthoSize = Camera.main.orthographicSize;
    }

    public void SetCameraViewSize(float size)
    {
        Camera.main.orthographicSize = size;
    }

    public void ZoomIn()
    {
        this.zoomIn = true;        
    }

    public void ZoomOut()
    {        
        this.zoomOut = true;
    }

    public void HaltZoom()
    {
        this.zoomOut = false;
        this.zoomIn = false;
    }

    public void ResetZoom()
    {
        Camera.main.orthographicSize = this.orthoSize; 
    }

    public void MoveCamera(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
    }


    // Update is called once per frame
    void Update()
    {
        // move x, y in movement direction and leave z alone
        Vector2 amount = movementDirection * Time.deltaTime * this.movementSpeed;
        Vector3 displacement = new Vector3(amount.x, amount.y, this.transform.localPosition.z);
        this.transform.localPosition += displacement;

        if (this.zoomIn)
        {
            Camera.main.orthographicSize = Mathf.Max(MinZoom, Camera.main.orthographicSize - Time.deltaTime);
        }

        if (this.zoomOut)
        {
            Camera.main.orthographicSize += Time.deltaTime; 
        }
    }
}
