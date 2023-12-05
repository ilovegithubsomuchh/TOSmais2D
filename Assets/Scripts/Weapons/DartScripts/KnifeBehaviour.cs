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
    }

    protected override void Update()
    {
        Attack();
        TimeBeforeDestroy += Time.deltaTime;
        if (TimeBeforeDestroy > 4) Destroy();
    }

    protected override void Attack()
    {
       transform.position += new Vector3((WeaponDirectionX * (WeaponData.speed * Time.deltaTime)), (WeaponDirectionY * (WeaponData.speed * Time.deltaTime)), 0);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}