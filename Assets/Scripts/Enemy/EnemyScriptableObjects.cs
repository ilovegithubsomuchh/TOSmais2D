using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "EnemyScriptableObjects", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObjects : ScriptableObject
{
    
    //Base stats enemies
    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }
    
    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
    
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }
    
    [SerializeField]
    float armor;
    public float Armor { get => armor; private set => armor = value; }
}
