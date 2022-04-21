using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    public GameManager GameManager {get {return _gameManager;}}
    [SerializeField] private GridMovement _gridMovement;
    public GridMovement GridMovement {get {return _gridMovement;}}
    [SerializeField] private Camera _cam;
    public Camera Cam {get {return _cam;}}
    [SerializeField] private Vector2 _playerWorldPos;
    public Vector2 PlayerWorldPos {get {return _playerWorldPos;} set {_playerWorldPos = value;}}
    
}
