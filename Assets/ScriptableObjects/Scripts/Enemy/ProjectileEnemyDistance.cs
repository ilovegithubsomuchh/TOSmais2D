using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyDistance : MonoBehaviour

{
    private Transform target;
    public float speed = 5f;
    public float maxDistance = 10f;

    private Vector3 initialDirection;
    private float distanceTraveled;

    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        
        if (target != null)
        {
            initialDirection = (target.position - transform.position).normalized;
        }
        else
        {
            initialDirection = transform.forward.normalized;
        }
    }

    void Update()
    {
        Vector3 newPosition = transform.position + initialDirection * speed * Time.deltaTime;
        transform.position = newPosition;
        distanceTraveled += speed * Time.deltaTime;
        if (distanceTraveled > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}


