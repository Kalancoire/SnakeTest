using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    #region UI
    [Header("Score UI")]
    [SerializeField] private int _score = 0;
    [SerializeField] private TextMeshProUGUI _scoreText;
    #endregion

    #region Pickups
    [Header("Pickups")]
    [SerializeField] private SpriteRenderer _pickupSpriteRenderer;
    [SerializeField] private PickupData _currentPickup;
    #endregion

    #region Grid Data
    [Header("Grid Data")]
    [SerializeField] [Range(5,18)] private int _width = 18;
    [SerializeField] [Range(5,10)] private int _height = 10;
    [SerializeField] [Range(0.5f, 2f)] private float _cellSize = 1f;
    private GridManager _gameState;
    #endregion

    #region Grid Values
    [Header("Grid Values")]
    [SerializeField] private List<PickupData> _pickupValues;
    private Dictionary<string, PickupData> _pickupDict;
    public string GridValueSnake {get {return "Snake";}}
    public string GridValueWall {get {return "Wall";}}
    public string GridValueEmpty {get {return "Empty";}}
    #endregion

    [SerializeField] private Queue<Vector3> _snakeBlobPositions = new Queue<Vector3>();
    private bool _gameStarted = false;
    public bool GameStarted { get{return _gameStarted;} set{_gameStarted = value;}}

    private void Awake()
    {
        _pickupDict = new Dictionary<string, PickupData>();
        for (int i = 0; i < _pickupValues.Count; i++)
        {
            _pickupDict[_pickupValues[i].name] = _pickupValues[i];
        }
    }

    private void Start()
    {
        UpdateScore(0);
        _gameState = new GridManager(_width, _height, _cellSize);
        UpdateGameStartState();
        UpdateCurrentPickup();
        SpawnPickup();
    }

    //Given a position, change the sprite renderer's position
    //Given a PickupDate, change sprite

    //"Spawns" a pickup via _currentPickup (visual)
    private void SpawnPickup()
    {
        Vector3 position = _gameState.GetRandomEmptyPosition();
        SpawnPickup(position);
    }

    private void SpawnPickup(Vector3 position)
    {
        _pickupSpriteRenderer.transform.position = position + new Vector3 (0.5f, 0.5f, 0f);

        if (_currentPickup == null)
        {
            _currentPickup = _pickupValues[0];
        }

        _pickupSpriteRenderer.sprite = _currentPickup.Sprite;
        SetValue(position, _currentPickup.name);
    }

    private void UpdateCurrentPickup(PickupData pickupData = null)
    {
        if (pickupData == null)
        {
            int randomIndex = Random.Range(0, _pickupValues.Count);
            _currentPickup = _pickupValues[randomIndex];
        }
        else
        {
            _currentPickup = pickupData;
        }
    }
   
    public void GetValue(Vector3 worldPosition)
    {
        _gameState.GetValue(worldPosition);
    }

    public void UpdateScore(int points)
    {
        _score += points;
        _scoreText.text = "Score: " + _score.ToString();
    }

    private void UpdateGameStartState()
    {
        Vector3 playerPosition = _playerData.transform.position;
        _snakeBlobPositions = new Queue<Vector3>();
        for (int i = _playerData.SnakeLength-1; i >= 0; i--)
        {
            Vector3 calculatedPos = new Vector3(playerPosition.x - i, playerPosition.y, playerPosition.z);
            SetValue(calculatedPos, GridValueSnake);
            _snakeBlobPositions.Enqueue(calculatedPos);
        }
    }

    public void UpdateGameState(Vector3 position)
    {
        string value = _gameState.GetValue(position);
        if ((value == GridValueSnake && position != _snakeBlobPositions.Peek()) || value == GridValueWall)
        {
            Debug.Log("Deadge");
            Time.timeScale = 0f;
            return;
        }
        else if (_pickupDict.ContainsKey(value))
        {
            int points = _pickupDict[value].Points;
            _playerData.IncreaseLength();
            UpdateScore(points);
            _gameState.SetValue(position, GridValueSnake);
            if (_playerData.SnakeLength != _gameState.TileCount)
            {
                SpawnPickup();
            }
            else
            {
                GameOver(win:true);
            }
        }
        else
        {
            _gameState.SetValue(position, GridValueSnake);
            _snakeBlobPositions.Enqueue(position);
            Vector3 removeSnakePosition = _snakeBlobPositions.Dequeue();
            _gameState.SetValue(removeSnakePosition, GridValueEmpty);
        }
    }

    public void SetValue(Vector3 worldPosition, string name)
    {
        _gameState.SetValue(worldPosition, name);
    }

    private void GameOver(bool win)
    {
        if (win)
        {
            Debug.Log("Win!");
        }
        else
        {
            Debug.Log("Deadge");
        }
    }
    
}
