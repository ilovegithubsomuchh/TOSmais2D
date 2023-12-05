using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : WeaponManager
{
    
    protected override void Start()
    {
        base.Start();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
       
        
    }


    private void LateUpdate()
    {

        Attack();
        
    }

    protected override void Attack()
    {
        transform.RotateAround(transform.parent.position, transform.parent.forward, WeaponData.speed * Time.deltaTime);
    }
    
    
}