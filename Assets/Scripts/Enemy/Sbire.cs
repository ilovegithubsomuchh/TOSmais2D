using System.Collections;
using UnityEngine;

public class Sbire : BaseEnemy
{
    private Player _player;
    private float AvoidanceForce = 2f;

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
           
    }

    float CalculateAvoidanceForce(float avoidanceRadius)
        {
            // You can adjust this formula or set AvoidanceForce directly based on your needs
            return AvoidanceForce / avoidanceRadius;
        }
  

    public override void Attack()
    {
       
       _player.TakeDamage(currentDamage);
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