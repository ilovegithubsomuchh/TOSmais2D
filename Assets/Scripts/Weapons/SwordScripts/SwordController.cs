using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : WeaponManager
{
    private int swordToSpawn;
    private GameObject Sword;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        for (int i = 0; swordToSpawn < WeaponData.NumberToSpawn; i++)
        {
            var radians = 2 * Mathf.PI / WeaponData.NumberToSpawn * i; // Player spawns a number of weapons determined in the specified weapon data 
            var vertical = Mathf.Sin(radians); // Calculation to position all swords in a circle around the player
            var horizontal = Mathf.Cos(radians);
            var position = new Vector3(horizontal, vertical, 0);
            var spawnpos = transform.parent.position + position * 2;
            Sword = Instantiate(WeaponData.prefab, spawnpos, Quaternion.identity);
            Sword.transform.SetParent(transform);

            // Calculate the rotation angle based on the current sword's position
            float rotationAngle = Mathf.Rad2Deg * radians;
            Sword.transform.Rotate(new Vector3(0, 0, rotationAngle));

            swordToSpawn++;
        }

        swordToSpawn = 0;
    }
}