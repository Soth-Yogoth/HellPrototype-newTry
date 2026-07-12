using System;
using UnityEngine;
using UnityEngine.Pool;

public class Projectail : MonoBehaviour
{
    [SerializeField] private int damage;
    
    public delegate bool HitMethod(Collider2D collider);
    public HitMethod Hit;
    
    public ObjectPool<GameObject> ObjectPool { get; set; }
    public bool IsReleased { get; set; }
    

    public void SetHostType(String targetTag)
    {
        Hit = targetTag == "Player" ? HitEnemy : HitPlayer;
    }

    bool HitPlayer(Collider2D playerCollider)
    {
        if (playerCollider.gameObject.CompareTag("Player"))
        {
            PlayerController player = playerCollider.gameObject.GetComponent<PlayerController>();
            player.GetHit();
            
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
        if (enemyCollider.gameObject.CompareTag("FinalLight"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = -rb.linearVelocity;
            
            if (GameData.ChanceToWin > 0)
            {
                GameData.ChanceToWin -= 1;
            }
            else
            {
                Debug.Log("Catch!");
                GameManager.GameOver();
            }
        }
        return false;
    }
    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        //TODO: но что если Hit == null ? 
        if (Hit(other) && !IsReleased)
        {
            ObjectPool.Release(gameObject);
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "GameScreen" && !IsReleased)
        {
            ObjectPool.Release(gameObject);
        }
    }
}
