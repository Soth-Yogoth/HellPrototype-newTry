using UnityEngine;

public class SimpleEnemy : BaseEnemy
{
    Rigidbody2D rb;

    private int id;
    bool isDead = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Awake()
    {
        base.Awake();
        
        rb = GetComponent<Rigidbody2D>();
        GameData.MobQuantity++;
        
        id = GameData.MobQuantity;
    }
    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GameScreen"))
        {
            bulletSpawner.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 direction = playerTransform.position - transform.position;
        direction.Normalize();
        
        rb.linearVelocity = direction * moveSpeed;
    }

    protected override void Death()
    {
        if(isDead) return;
        
        base.Death();
        
        isDead = true;
        GameData.MobQuantity--;
    }
}
