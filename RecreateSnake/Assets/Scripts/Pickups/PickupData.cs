using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup")]
public class PickupData : ScriptableObject
{
    [SerializeField] [Range(0,50)] private int _points;
    public int Points {get {return _points;}}
}
