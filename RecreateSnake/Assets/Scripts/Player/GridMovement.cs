using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private bool _isMoving;
    private Vector2 _origPos, _targetPos;
    [SerializeField] private float _moveTime;

    public IEnumerator MovePlayer(Vector2 direction)
    {
            _isMoving = true;

            float elapsedTime = 0;
            _origPos = transform.position;
            _targetPos = _origPos + direction;

            while (elapsedTime < _moveTime)
            {
                transform.position = Vector3.Lerp(_origPos, _targetPos, (elapsedTime/_moveTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = _targetPos;
            _isMoving = false;

    }

    private void Update()
    {
        if(_isMoving == false)
        {
            StartCoroutine(MovePlayer(_playerData.GetDirection(true)));
        }
    }

}
