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
    [SerializeField]
    protected float distanceToPlayer;
    
    public abstract void Move();

    public abstract void Attack();

    public abstract void TakeDamage(float dmg);

    public abstract void Kill();
    
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
