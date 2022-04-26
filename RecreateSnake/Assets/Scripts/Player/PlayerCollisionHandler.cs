using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] LayerMask _pickupMask;
    [SerializeField] private PlayerData _playerData;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((_pickupMask & (1 << other.gameObject.layer)) != 0) //If bitmask value of layer matches layermask
        {
            int points = other.GetComponentInParent<Pickup>().GetPoints();
            _playerData.GameManager.UpdateScore(points);
            _playerData.IncreaseLength();
            
        }
    }
}
