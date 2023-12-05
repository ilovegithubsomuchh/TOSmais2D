using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponsSO", menuName = "ScriptableObjects/weapons")]
public class WeaponSO : ScriptableObject
{
    public GameObject prefab;
    public float damage;
    public float speed;
    public float cooldownDuration;
    public GameObject NextUpgrade;
    public int NumberToSpawn;
}