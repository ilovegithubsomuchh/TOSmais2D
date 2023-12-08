using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = Microsoft.Unity.VisualStudio.Editor.Image;


[CreateAssetMenu(fileName = "WeaponsSO", menuName = "ScriptableObjects/weapons")]
public class WeaponSO : ScriptableObject
{
    public Image icon;
    public GameObject prefab;
    public int level;
    public float damage;
    public float speed;
    public int NumberToSpawn;
    public float cooldownDuration;
    public float TimeBeforeDestruction;
    public GameObject NextUpgrade;
    public String UpgradeDescription;
}