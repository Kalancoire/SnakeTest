using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private string _mouseMoveActionString = "MousePos";
    [SerializeField] private string _mouseClickActionString = "MouseClick";
    private InputAction _mouseMoveAction;
    private InputAction _mouseClickAction;

    [SerializeField] private Vector3 _mouseWorldPos;

    private void Awake()
    {
        _mouseMoveAction = _playerInput.actions[_mouseMoveActionString];
        _mouseClickAction = _playerInput.actions[_mouseClickActionString];
    }

    private void OnEnable()
    {
        _mouseMoveAction.performed += OnMouseMove;

        _mouseClickAction.started += OnMouseClick;
    }

    private void OnDisable()
    {
        _mouseMoveAction.performed -= OnMouseMove;

        _mouseClickAction.started -= OnMouseClick;
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPos = context.ReadValue<Vector2>();
        _mouseWorldPos = _playerData.Cam.ScreenToWorldPoint(mouseScreenPos);
        
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        _playerData.GameManager.SetValue(_mouseWorldPos, 2);
    }
    
}
