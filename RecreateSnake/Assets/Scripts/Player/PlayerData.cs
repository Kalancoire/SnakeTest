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
    [SerializeField] private Camera _cam;
    public Camera Cam {get {return _cam;}}
    #endregion

    #region Variables
    [Header("Variables")]
    [SerializeField] private Vector2 _currentDirection;
    [SerializeField] private int _snakeLength = 1;
    private Queue<Vector2> _queuedDirections = new Queue<Vector2>();
    #endregion

    #region Functions

    public bool IsInputAxisApplicable(Vector2 direction)
    {
        if((Mathf.Abs(direction.x) >= 0.5f && Mathf.Abs(_currentDirection.x) >= 0.5f) ||
            (Mathf.Abs(direction.y) >= 0.5f && Mathf.Abs(_currentDirection.y) >= 0.5f))
        {
            return true;
        }
        else return false;
    }
    public Vector2 GetDirection(bool dequeue = false)
    {
        if(_queuedDirections.Count > 0) //If a direction is queued
        {   
            if(dequeue) //If we want to get a direction from queue
            {
                Vector2 temp = _queuedDirections.Dequeue();

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
        
        if (Mathf.Abs(direction.x) >= 0.1f)
        {
            _queuedDirections.Enqueue(new Vector2(direction.x, 0));
        }

        if (Mathf.Abs(direction.y) >= 0.1f)
        {
            _queuedDirections.Enqueue(new Vector2(0, direction.y));
        }
    }

    public void IncreaseLength()
    {
        _snakeLength++;
        //Update Snake visuals
        _gameManager.UpdateSnakePosition();
    }
    #endregion
}
