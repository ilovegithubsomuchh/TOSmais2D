using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ScytheController : WeaponManager
{
    private GameObject Scythe;
    private int SpawnedScythe;
   
    

    // Update is called once per frame
    protected override void Attack()
    {
        base.Attack();
        Scythe = Instantiate(WeaponData.prefab, transform.position, quaternion.identity);
        Scythe.transform.SetParent(transform);
        SpawnedScythe++;
        if (SpawnedScythe % 3 == 0)
        {
            Scythe.transform.SetParent(null);
        }
    }
    
    
}
