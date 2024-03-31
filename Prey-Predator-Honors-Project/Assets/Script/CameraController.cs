using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Multiplier. Should not be 0.0")]
    private float movementSpeed = 1.0f;
    // records the input movement direction 
    private Vector2 movementDirection = Vector2.zero;
    private bool zoomIn = false;
    private bool zoomOut = false;

    public void ZoomIn()
    {
        this.zoomIn = true;
        Debug.Log("Zooming");
    }

    public void ZoomOut()
    {
        Debug.Log("Zooming");
        this.zoomOut = true;
    }

    public void HaltZoom()
    {
        this.zoomOut = false;
        this.zoomIn = false;
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
    }
}
