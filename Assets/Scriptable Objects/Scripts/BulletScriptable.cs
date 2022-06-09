using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Bullet Type", menuName = "Ammo/Bullet", order =1)]
public class BulletScriptable : ScriptableObject
{
    [Header("Bullet Settings")]
    public GameObject BulletPrefab;
    [Range(10f, 50f)]
    public float Speed;
}
