using UnityEngine;

public class SimpleEnemy : BaseEnemy
{
    Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
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
    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MobFinalLine"))
        {
            bulletSpawner.SetActive(true);
            isReady = true;
        }
    }
}
