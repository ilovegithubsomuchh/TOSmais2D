using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public EnemyScriptableObjects enemyData;

    protected Transform playerTransform;
    protected GameObject player;

    protected float currentMoveSpeed;
    protected float currentHealth;
    protected int currentDamage;
    protected bool canDamage;

    protected static int killedEnemy;
    protected bool dead;
    [SerializeField]
    protected float distanceToPlayer;
    
    public abstract void Move();

    public abstract void Attack();

    public virtual void TakeDamage(float dmg)
    {
             
        if (currentHealth < 0 && !dead)
        {
            dead = true;
            Debug.Log("mort");
            Kill();
        }
    }

    public virtual void Kill()
    {
        killedEnemy++;
    }
    
    public virtual void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }
    
    public virtual void Update()
    {
        Move();
    }
    
    public static int CountKilledEnemy()
    {
        
        return killedEnemy;
    }
}
