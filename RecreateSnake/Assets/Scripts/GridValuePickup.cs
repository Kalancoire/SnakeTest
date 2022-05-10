using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridValue/Pickup", fileName = "New Grid Value Pickup")]
public class GridValuePickup : GridValue
{
[SerializeField] private int _points;
public int Points {get{return _points;}}

[SerializeField] private Sprite _sprite;
public Sprite Sprite {get{return _sprite;}}
}
