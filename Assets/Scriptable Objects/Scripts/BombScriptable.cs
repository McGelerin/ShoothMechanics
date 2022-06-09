using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bomb Type", menuName = "Ammo/Bomb", order =2)]
public class BombScriptable : ScriptableObject
{
    [Header("Bomb Settings")]
    public GameObject BombPrefab;
    [Range(10f, 50f)]
    public float Speed;
    [Range(0.5f, 2f)]
    public float Radius;
    public float ExplosionForce = 50;
}
