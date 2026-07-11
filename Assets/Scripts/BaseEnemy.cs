using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] [Min(1)] protected int maxHp;
    [SerializeField] [Min(1)] protected int moveSpeed;
    
    [SerializeField] [Min(1)] protected int health;
    
    protected static Transform playerTransform;
    public static Transform PlayerTransform { set { playerTransform = value; } } //Вызов из GameManager

    protected void Start()
    {
        health = maxHp;
    }
    
    public virtual void TakeDamage(int damage)
    {
        if (health - damage <= 0) Death();
        else health -= damage;
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }
}
