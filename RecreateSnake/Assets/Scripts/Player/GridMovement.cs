using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private float _moveTime;
    public float MoveTime {get {return _moveTime;}}
    private Vector2 _origPos, _targetPos;

    public IEnumerator MovePlayer(Vector2 direction)
    {
        _playerData.IsMoving = true;
        Vector3 currentDirection = new Vector3(direction.x, 0, 0);

        float elapsedTime = 0;
        _origPos = transform.position;
        _targetPos = _origPos + direction;

        _playerData.GameManager.UpdateGameState(_targetPos);

        while (elapsedTime < _moveTime)
        {
            transform.position = Vector3.Lerp(_origPos, _targetPos, (elapsedTime/_moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.position = _targetPos;

        _playerData.IsMoving = false;

    }

    private void Update()
    {
        if(_playerData.IsMoving == false && _playerData.GameManager.GameStarted)
        {
            StartCoroutine(MovePlayer(_playerData.GetDirection(true)));
        }
    }
}
