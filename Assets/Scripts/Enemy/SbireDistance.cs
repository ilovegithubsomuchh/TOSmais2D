using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class SbireDistance : BaseEnemy
{
    [SerializeField] private GameObject projectileGO;

    private bool inRange;
    private bool isShooting;

    private void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
        isShooting = true;
    }
    
    public override void Move()
    {
        if (!inRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.transform.position,enemyData.MoveSpeed * Time.deltaTime);
            
        }
    }
    
    public override void Attack()
    {
        isShooting = false;
        Instantiate(projectileGO, transform.position, quaternion.identity);
       
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
    
    public override void Update()
    {
        base.Update();
        float distance = Vector2.Distance(playerTransform.transform.position, transform.position);
        if (distance < distanceToPlayer)
        {
            inRange = true;
            StartCoroutine(Projectile());
        }
        else
        {
            inRange = false;
        }
    } 

    IEnumerator Projectile()
    {
        while (inRange && isShooting)
        {
            Attack();
            yield return new WaitForSeconds(1f);
            isShooting = true;

        }
    }
    
}
