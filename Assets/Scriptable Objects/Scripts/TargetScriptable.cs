using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Target Type", menuName = "Target", order = 2)]
public class TargetScriptable : ScriptableObject
{
    [Header("Target Prefab")]
    public GameObject TargetPrefab;
    public GameObject BrokenTargetPrefab;


    [Header("Matris Settings")]
    public int Column;
    public int Row;
    [Range(.5f,3)]
    public float Offset;

    [Header("Camera Offset Coefficient")]
    [Range(2,5)]
    public int Coefficient;

}
