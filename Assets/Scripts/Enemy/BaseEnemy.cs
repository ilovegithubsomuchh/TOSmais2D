using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public EnemyScriptableObjects enemyData;

    protected Transform playerTransform;
    protected GameObject player;

    protected float currentMoveSpeed;
    protected float currentHealth;
    protected float currentDamage;
    
    public abstract void Move();

    public abstract void Attack();

    public abstract void TakeDamage(float dmg);

    public abstract void Kill();
    
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }
    
    void Update()
    {
        Move();
    }
}
