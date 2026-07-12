using UnityEngine;

public class SimpleEnemy : BaseEnemy
{
    [SerializeField] protected GameObject bulletSpawner;
    
    Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        
        rb = GetComponent<Rigidbody2D>();
        GameData.MobQuantity++;
    }
    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "GameScreen")
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
        base.Death();
        GameData.MobQuantity--;
    }
}
