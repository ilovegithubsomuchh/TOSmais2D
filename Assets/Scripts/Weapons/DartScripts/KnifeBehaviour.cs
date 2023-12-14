using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class KnifeBehaviour : WeaponManager
{
    public PlayerMovement PlayerMovement;
    private float WeaponDirectionX;
    public float WeaponDirectionY;


    protected override void Start()
    {
        base.Start();

        PlayerMovement = FindObjectOfType<PlayerMovement>();
        WeaponDirectionX = PlayerMovement._inputValueX;
        WeaponDirectionY = PlayerMovement._inputValueY;
        if ((WeaponDirectionX == 0) && (WeaponDirectionY == 0))
        {
            WeaponDirectionX = 1;
        }

        _destroy = true;
    }

    protected override void Update()
    {
        Attack();
        if (_destroy) TimeBeforeDestroy += Time.deltaTime;
        if (TimeBeforeDestroy > WeaponData.TimeBeforeDestruction) Destroy();
    }

    protected override void Attack()
    {
        transform.position += new Vector3((WeaponDirectionX * (WeaponData.speed * Time.deltaTime)),
            (WeaponDirectionY * (WeaponData.speed * Time.deltaTime)), 0);
    }

    protected override void Destroy()
    {
        Destroy(gameObject);
    }
   
    

    
}