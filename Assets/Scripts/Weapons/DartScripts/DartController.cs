using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DartController : WeaponManager
{
    private int dartToSpawn;
    private GameObject Dart;


    protected override void Attack()
    {
       
        base.Attack();
        for (int i = 0; dartToSpawn < WeaponData.NumberToSpawn; i++)
        {
            var radians = 2 * Mathf.PI / WeaponData.NumberToSpawn * i;
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);
            var position = new Vector3(horizontal, vertical, 0);
            var spawnpos = transform.parent.position + position * 2;
            Dart = Instantiate(WeaponData.prefab, spawnpos, Quaternion.identity);
            dartToSpawn++;
        }

        dartToSpawn = 0;
    }
}