using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : WeaponManager
{
    protected override void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        _destroy = true;
    }


    protected override void Update()
    {
        base.Update();

        Attack();
        if (_destroy) TimeBeforeDestroy += Time.deltaTime;
        if (TimeBeforeDestroy > WeaponData.TimeBeforeDestruction)
            Destroy(); // destroy the swords after a determined time in the Weapon Data
    }

    protected override void Attack()
    {
        transform.RotateAround(transform.parent.position, transform.parent.forward,
            WeaponData.speed * Time.deltaTime); // make them rotate around the player 
    }

    protected override void Destroy()
    {
        Destroy(gameObject);
    }
}