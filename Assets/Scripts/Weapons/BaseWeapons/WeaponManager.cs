using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Global Weapon Variables")] public WeaponSO WeaponData;
    private float CurrentCoolDown;
    protected float TimeBeforeDestroy;
    protected bool _destroy;

    protected virtual void Start()
    {
        CurrentCoolDown = WeaponData.cooldownDuration; // Init cooldown with the one inside the weapon data  
        // TimeBeforeDestroy = WeaponData.TimeBeforeDestruction;
    }


    protected virtual void Update()
    {
        CurrentCoolDown -= Time.deltaTime;
        if (CurrentCoolDown <= 0f) Attack(); // Timer between each attack
    }

    protected virtual void Attack()
    {
        CurrentCoolDown = WeaponData.cooldownDuration; // Reset attack timer
    }

    protected virtual void Destroy()
    {
    }
}