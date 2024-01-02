using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herald : BaseEnemy
{
    [SerializeField]
    protected float chargeCooldown;
    [SerializeField]
    protected float chargeSpeed;
    [SerializeField]
    protected float detectionRange;

    private bool isCharging = false;

    public override void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.transform.position,
            enemyData.MoveSpeed * Time.deltaTime);
    }

    public override void Attack()
    {
        
    }

    public void ChargePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        
        if (distanceToPlayer <= detectionRange)
        {
            isCharging = true;
            Invoke("StopCharging", 1f);
            
        }
    }
    
    void StopCharging()
    {
        isCharging = false;
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
        if (isCharging)
        {
            Debug.Log("destroy");
            //Destroy(player);
            
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, chargeSpeed * Time.deltaTime);
            
        }
    }

    public override void Start()
    {
        base.Start();
        InvokeRepeating("ChargePlayer", 2f, chargeCooldown);
    }
}
