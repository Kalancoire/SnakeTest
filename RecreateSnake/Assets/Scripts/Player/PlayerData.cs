using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Camera _cam;
    public GameManager GameManager {get {return _gameManager;}}
    public Camera Cam {get {return _cam;}}


}
