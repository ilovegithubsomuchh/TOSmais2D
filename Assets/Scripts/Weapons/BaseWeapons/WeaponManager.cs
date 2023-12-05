using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponSO WeaponData;
    private float CurrentCoolDown;
    protected float offsetX;
    protected float offsetY;

    protected virtual void Start()
    {
       
    }


    protected virtual void Update()
    {
        CurrentCoolDown -= Time.deltaTime;
        if (CurrentCoolDown <= 0f)
        {
            Attack();
            
        }
    }

    protected virtual void Attack()
    {
        CurrentCoolDown = WeaponData.cooldownDuration;
        
    }
}