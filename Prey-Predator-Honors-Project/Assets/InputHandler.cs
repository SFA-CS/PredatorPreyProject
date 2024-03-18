using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.canceled)  // button released
        {
            // ray cast to determine where the click happened
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray);
            if (rayHit.collider != null)
            {
                Debug.Log(rayHit.collider.gameObject.name);                
            }
            Debug.Log(mousePosition);
            Debug.Log(ray.origin);
           
        }
    }
}
