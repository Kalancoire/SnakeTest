using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GridManager _gameState;
    [SerializeField] [Range(5,18)] private int _width = 18;
    [SerializeField] [Range(5,10)] private int _height = 10;
    [SerializeField] [Range(0.5f, 2f)] private float _cellSize = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameState = new GridManager(_width, _height, _cellSize);
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        _gameState.SetValue(worldPosition, value);
    }

}
