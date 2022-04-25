using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerInput _playerInput;

    [Header("Mouse Movement")]
    [SerializeField] private string _mouseMoveActionString = "MousePos";
    [SerializeField] private Vector3 _mouseWorldPos;
    private InputAction _mouseMoveAction;

    [Header("Mouse Click")]
    [SerializeField] private string _mouseClickActionString = "MouseClick";
    private InputAction _mouseClickAction;

    [Header("Player Movement")]
    [SerializeField] private string _playerMovementString = "Movement";
    [SerializeField] private float _maxInputTime;
    [SerializeField] private float _minInputTime;
    private float _currentInputTime;
    private InputAction _playerMovement;

    private void Awake()
    {
        _mouseMoveAction = _playerInput.actions[_mouseMoveActionString];
        _mouseClickAction = _playerInput.actions[_mouseClickActionString];
        _playerMovement = _playerInput.actions[_playerMovementString];
    }

    private void Update()
    {
        if(_currentInputTime < _maxInputTime)
        {
            _currentInputTime += Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        _mouseMoveAction.performed += OnMouseMove;

        _mouseClickAction.started += OnMouseClick;

        _playerMovement.performed += OnMovement;
    }

    private void OnDisable()
    {
        _mouseMoveAction.performed -= OnMouseMove;

        _mouseClickAction.started -= OnMouseClick;

        _playerMovement.performed -= OnMovement;
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

    private void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        Vector2 roundedVector = new Vector2Int(Mathf.RoundToInt(inputVector.x), Mathf.RoundToInt(inputVector.y));
        if(_currentInputTime <= _maxInputTime && _currentInputTime >= _minInputTime)
        {
            _playerData.AddDirection(roundedVector, false);
            _currentInputTime = 0f;
        }
        else if (_currentInputTime >= _maxInputTime)
        {
            _playerData.AddDirection(roundedVector, true);
            _currentInputTime = 0f;
        }

    }
    
}
