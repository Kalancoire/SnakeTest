    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager
{
    [SerializeField] [Range(5,18)] private int _width;
    [SerializeField] [Range(5,10)] private int _height;
    public int TileCount {get {return _width * _height;}}
    [SerializeField] private float _cellSize;
    private float _offSetWidth;
    private float _offSetHeight;
    private string [,] _gridArray; 
    public GridManager(int width, int height, float cellSize)
    {
        _width = width;
        _offSetWidth = -1 * (_width/2f);
        _height = height;
        _offSetHeight = -1 * (_height/2f);
        _cellSize = cellSize;

        _gridArray = new string[width, height];

        //Cycle through _gridArray; Current: draw Gizmos to make sure it's placed correctly
        for(int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < _gridArray.GetLength(1); y++)
            {
                SetValue(x, y, "Empty");
            }
        }
    }

    //Get worldPos via int x and int y
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x + _offSetWidth, y + _offSetHeight) * _cellSize;
    }

    //Set the value of a tile located at (x,y) via int value
    public void SetValue(int x, int y, string value)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
           _gridArray[x,y] = value;
        }

        //Debugging
        Color color;
        if(value == "Empty")
        {
            color = Color.white;
        }
        else if(value == "Snake")
        {
            color = Color.green;
        }
        else
        {
            color = Color.magenta;
        }
        Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x+1,y+1), color, 2f);
    }

    public void SetValue(Vector3 worldPosition, string value)
    {
        int x,y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public string GetValue(Vector3 worldPosition)
    {
        int x,y;
        GetXY(worldPosition, out x, out y);
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return _gridArray[x,y];
        }
        else 
        {
            return "Wall";
        } 
    }

    private bool IsTileEmpty(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return _gridArray[x,y] == "Empty";
        }
        else
        {
            return false;
        }
    }

    //Get x & y values via Vector3 worldPosition
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / _cellSize - _offSetWidth);
        y = Mathf.FloorToInt(worldPosition.y / _cellSize - _offSetHeight);
    }

    public Vector3 GetRandomEmptyPosition()
    {
        List<Vector2Int> emptyTiles = new List<Vector2Int>();
        for(int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < _gridArray.GetLength(1); y++)
            {
                if (IsTileEmpty(x,y))
                {
                    emptyTiles.Add(new Vector2Int(x,y));
                }
            }
        }

        if (emptyTiles.Count == 0)
        {
            return new Vector3(int.MaxValue, int.MaxValue, int.MaxValue);
        }
        else
        {
            Vector2Int randomTile = emptyTiles[Random.Range(0, emptyTiles.Count)];
            return GetWorldPosition(randomTile.x, randomTile.y);
        }

    }
}
