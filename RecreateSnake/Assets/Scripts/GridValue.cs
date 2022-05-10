using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridValue/Grid Value", fileName = "New Grid Value")]
public class GridValue : ScriptableObject
{
    [SerializeField] private int _value;
    public int Value {get{return _value;}}
}
