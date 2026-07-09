using System;
using UnityEngine;
using UnityEngine.Pool;

public class Projectail : MonoBehaviour
{
    [SerializeField] private int damage;
    
    public delegate bool HitMethod(Collider2D collider);
    public HitMethod Hit;
    
    private ObjectPool<GameObject> objectPool;
    public ObjectPool<GameObject> ObjectPool { set => objectPool = value; }

    public void SetHostType(String targetTag)
    {
        Hit = targetTag == "Player" ? HitEnemy : HitPlayer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //TODO: но что если Hit == null ? 
        if (Hit(other)) objectPool.Release(gameObject);
    }

    bool HitPlayer(Collider2D playerCollider)
    {
        if (playerCollider.gameObject.CompareTag("Player"))
        {
            PlayerController player = playerCollider.gameObject.GetComponent<PlayerController>();
            player.GetHit(damage);
            
            return true;
        }
        return false;
    }

    bool HitEnemy(Collider2D enemyCollider)
    {
        if (enemyCollider.gameObject.CompareTag("Enemy"))
        {
            BaseEnemy enemy = enemyCollider.gameObject.GetComponent<BaseEnemy>();
            enemy.TakeDamage(damage);
            
            return true;
        }
        return false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "GameScreen")
        {
            objectPool.Release(gameObject);
        }
    }
}
