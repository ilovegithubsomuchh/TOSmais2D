using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sbire : BaseEnemy
{
    private Player _player;
    private int dmg = 10;
    
    public override void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.transform.position,
            enemyData.MoveSpeed * Time.deltaTime);
    }

    public override void Attack()
    {
       
       _player.TakeDamage(dmg);
    }

    public override void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth < 0)
        {
            Kill();
        }
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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.CompareTag("Player"))
        {
            canDamage = true;
            _player = other.GetComponent<Player>();
            StartCoroutine(DealDamages());
           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            canDamage = false;
        }
    }

    public override void Update()
    {
        base.Update();
        
    }
    
    IEnumerator DealDamages()
    {
        
        while (canDamage)
        {
            yield return new WaitForSeconds(0.4f);
            Attack();
        }
    }
}