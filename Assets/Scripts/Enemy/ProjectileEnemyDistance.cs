using System;
using UnityEngine;

public class ProjectileEnemyDistance : MonoBehaviour

{
    
    public float speed = 5f;
    public float maxDistance = 10f;

    [SerializeField] private int dmg;
    
    private Transform target;
    private Vector3 initialDirection;
    private float distanceTraveled;
    private Player _player;
    

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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _player = col.GetComponent<Player>();
            _player.TakeDamage(dmg);
            Destroy(gameObject);
            
            
        }
        Debug.Log("here");
    }
}


