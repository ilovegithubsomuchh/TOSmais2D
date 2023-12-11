using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sbire : BaseEnemy
{
    public override void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.transform.position,enemyData.MoveSpeed * Time.deltaTime);
    }

    public override void Attack()
    {
        Debug.Log("destroy");
        //Destroy(player);
    }

    public override void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        /*if (currentHealth < 0)
        {
            Kill();
            
        } */
    }

    public override void Kill()
    {

        Destroy(gameObject);
        killedEnemy++;
        
        
    }

    private void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Non");
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Oui");
            player = col.gameObject;
            Attack();
        }
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Kill();
        }
    }

    
}
