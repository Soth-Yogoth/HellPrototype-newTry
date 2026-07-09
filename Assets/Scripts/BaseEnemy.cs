using System;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] [Min(1)] protected int health;
    [SerializeField] [Min(1)] protected int moveSpeed;
    //[SerializeField] protected GameObject bulletSpawner;
    
    protected static Transform playerTransform;
    public static Transform PlayerTransform { set { playerTransform = value; } } //Вызов из PlayerController

    // protected void Start()
    // {
    //     bulletSpawner.gameObject.SetActive(false);
    // }

    // protected void OnTriggerEnter2D(Collider2D other)
    // {
    //     Debug.Log("OnTriggerEnter2D");
    //     if (other.tag == "GameScreen")
    //     {
    //         bulletSpawner.gameObject.SetActive(true);
    //     }
    // }

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
