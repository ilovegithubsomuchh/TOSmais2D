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
        Vector2 targetPosition = playerTransform.transform.position;

        // Check if there is an obstacle in the path
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPosition - (Vector2)transform.position, Vector2.Distance(transform.position, targetPosition), LayerMask.GetMask("Ennemy"));

        if (hit.collider == null)
        {
            // If no obstacle, move towards the player
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemyData.MoveSpeed * Time.deltaTime);
        }
    }
    public override void Attack()
    {
       
       _player.TakeDamage(currentDamage);
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