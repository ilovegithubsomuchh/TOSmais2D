using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponSO WeaponData;
    protected float CurrentCoolDown;
    protected float offsetX;
    protected float offsetY;
    protected float TimeBeforeDestroy;
    protected bool _destroy;

    protected virtual void Start()
    {
        CurrentCoolDown = WeaponData.cooldownDuration;
       // TimeBeforeDestroy = WeaponData.TimeBeforeDestruction;
    }


    protected virtual void Update()
    {
        CurrentCoolDown -= Time.deltaTime;
        if(CurrentCoolDown <= 0f)Attack();
        
    }

    protected virtual void Attack()
    {
        CurrentCoolDown = WeaponData.cooldownDuration;
       

    }

    protected virtual void Destroy()
    {
        
    }



}