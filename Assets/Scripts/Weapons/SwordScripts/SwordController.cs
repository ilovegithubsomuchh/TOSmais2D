using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : WeaponManager
{
    private float TimeBeforeDestroy;
    private int swordToSpawn;
    private GameObject Sword;
    protected override void Attack()
    {
        for (int i = 0; swordToSpawn < WeaponData.NumberToSpawn; i++)
        {
            var radians = 2 * Mathf.PI / WeaponData.NumberToSpawn * i;
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);
            var position = new Vector3(horizontal, vertical, 0);
            var spawnpos = transform.parent.position + position * 2;
            Sword = Instantiate(WeaponData.prefab, spawnpos, Quaternion.identity);
            Sword.transform.SetParent(transform);
            swordToSpawn++;
        }


        
    }

   

    protected override void Update()
    {
        base.Update();
        TimeBeforeDestroy += Time.deltaTime;
        if(TimeBeforeDestroy > 4) Destroy();

    }

    private void Destroy()
    {
       
        for (int i = 0; i < WeaponData.NumberToSpawn; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        swordToSpawn = 0;
        TimeBeforeDestroy = 0;
        base.Attack();
       

    }
}