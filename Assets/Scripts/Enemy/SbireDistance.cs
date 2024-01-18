using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class SbireDistance : BaseEnemy
{
    [SerializeField] private GameObject projectileGO;

    private bool inRange;
    private bool isShooting;
    private float AvoidanceForce = 2f;

    private void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }
    
    public override void Move()
    {
        Vector2 targetPosition = playerTransform.transform.position;

     

        // Check if there are other enemies in the vicinity
        float avoidanceRadius = Mathf.Max(GetComponent<Rigidbody2D>().transform.localScale.x,
            GetComponent<Rigidbody2D>().transform.localScale.y);
        Collider2D[] colliders =
            Physics2D.OverlapCircleAll(transform.position, avoidanceRadius, LayerMask.GetMask("Enemy"));

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject) // Exclude the current enemy
            {
                // If there is another enemy in the vicinity, adjust the target position
                Vector2 avoidanceVector = transform.position - collider.bounds.center;
                targetPosition += avoidanceVector.normalized * CalculateAvoidanceForce(avoidanceRadius);
                
            }
            else
            {
                GetComponent<Rigidbody2D>().MovePosition(Vector2.MoveTowards(transform.position, targetPosition,
                    enemyData.MoveSpeed * Time.deltaTime));
            }
        }
        if (!inRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.transform.position,enemyData.MoveSpeed * Time.deltaTime);
            
        }
    }
    float CalculateAvoidanceForce(float avoidanceRadius)
    {
        // You can adjust this formula or set AvoidanceForce directly based on your needs
        return AvoidanceForce / avoidanceRadius;
    }
    
    public override void Attack()
    {
        isShooting = true;
        Instantiate(projectileGO, transform.position, quaternion.identity);
       
    }
    
    public override void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        base.TakeDamage(dmg);
    }
    
    public override void Kill()
    {
        Destroy(gameObject);
        base.Kill();
        
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
        while (inRange && !isShooting)
        {
            Attack();
            yield return new WaitForSeconds(1f);
            isShooting = false;

        }
    }
    
}
