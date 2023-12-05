using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DartController : WeaponManager
{


        protected override void Attack()
        {
                
                base.Attack();
                GameObject Dart = Instantiate(WeaponData.prefab);
                Dart.transform.position = transform.position;


        }

    
}