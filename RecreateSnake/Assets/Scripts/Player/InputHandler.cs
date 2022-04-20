using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public void OnHover(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("Hi");
            //Do something, e.g upward force for jump
        }
    }
}
