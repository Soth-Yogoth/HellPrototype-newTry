using System;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] [Min(1)] protected int health;
    [SerializeField] [Min(1)] protected int moveSpeed;
    
    protected static Transform playerTransform;
    public static Transform PlayerTransform { set { playerTransform = value; } } //Вызов из GameManager

    public void TakeDamage(int damage)
    {
        if (health - damage <= 0) Death();
        else health -= damage;
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }
}
