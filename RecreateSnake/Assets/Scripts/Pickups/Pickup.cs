using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupData _data;

    public int GetPoints()
    {
        return _data.Points;
    }
    
}
