using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    #region Components
    [Header("Components")]
    [SerializeField] private GameManager _gameManager;
    public GameManager GameManager {get {return _gameManager;}}
    [SerializeField] private GridMovement _gridMovement;
    public GridMovement GridMovement {get {return _gridMovement;}}
    [SerializeField] private GridManager _gridManager;
    public GridManager GridManager {get {return _gridManager;}}
    [SerializeField] private Camera _cam;
    public Camera Cam {get {return _cam;}}
    #endregion
    
    #region Variables
    [Header("Variables")]
    [SerializeField] private Vector3 _currentDirection;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private int _snakeLength = 4;
    public int SnakeLength {get {return _snakeLength;}}
    [SerializeField] private float _timePerBlob = 0.1f;
    [SerializeField] private bool _isMoving;
    public bool IsMoving {get {return _isMoving;} set {_isMoving = value;}}
    private Queue<Vector3> _queuedDirections = new Queue<Vector3>();
    #endregion

    #region Functions

    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        _timePerBlob = _gridMovement.MoveTime;
        _trailRenderer.time = (_snakeLength - 1) * _timePerBlob;
    }

    public void IncreaseLength()
    {
        _snakeLength++;
        _trailRenderer.time += _timePerBlob;
    }

    public bool IsInputAxisApplicable(Vector2 direction)
    {
        if((Mathf.Abs(direction.x) >= 0.5f && Mathf.Abs(_currentDirection.x) >= 0.5f) ||
            (Mathf.Abs(direction.y) >= 0.5f && Mathf.Abs(_currentDirection.y) >= 0.5f))
        {
            return true;
        }
        else return false;
    }

    public Vector3 GetDirection(bool dequeue = false)
    {
        if(_queuedDirections.Count > 0) //If a direction is queued
        {   
            if(dequeue) //If we want to get a direction from queue
            {
                Vector3 temp = _queuedDirections.Dequeue();

                if (IsInputAxisApplicable(temp) == false) //Make sure we're on opposite axes
                {
                    _currentDirection = temp; //Set direction to top of queue
                }
            }
            else return _queuedDirections.Peek(); //Else peek at top of queue
        }
        return _currentDirection; //return direction
    }
    
    public void AddDirection(Vector2 direction, bool clearQueue = false)
    {
        if (clearQueue)
        {
            _queuedDirections.Clear();
        }

        if ((Mathf.Abs(direction.x) >= 0.5f) && Mathf.Abs(direction.y) >= 0.5f)
        {
            return;
        }
        
        if (Mathf.Abs(direction.x) >= 0.5f)
        {
            _queuedDirections.Enqueue(new Vector3(Mathf.Sign(direction.x) * 1f, 0f));
        }

        if (Mathf.Abs(direction.y) >= 0.5f)
        {
            _queuedDirections.Enqueue(new Vector3(0f, Mathf.Sign(direction.y) * 1f));
        }
    }

    #endregion
}
