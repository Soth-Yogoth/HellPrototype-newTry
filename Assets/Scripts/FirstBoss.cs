using UnityEngine;

public class FirstBoss : BaseEnemy
{
    [SerializeField] [Min(0)] private float specialCooldown;
    private float cooldownTimer;
    
    [SerializeField] [Min(0)] private float movementBorder = 5; //Решение временное
    private Vector2 targetAncor;
    private bool isMovingToRight = true;

    private Rigidbody2D rb;

    void Start()
    {
        base.Start();
        
        cooldownTimer = specialCooldown;
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            cooldownTimer = specialCooldown;
            bulletSpawner.gameObject.SetActive(false);
            Special();
        }
        
        Debug.Log(playerTransform);
        Move();
    }

    private void Move()
    {
        //Ленивое решение
        if (transform.position.x > movementBorder) isMovingToRight = false;
        else if (transform.position.x < -movementBorder) isMovingToRight = true;
        
        Vector2 direction = isMovingToRight ? Vector2.right : Vector2.left;
        direction.Normalize();
        
        rb.linearVelocity = direction * moveSpeed;
    }

    private void Special()
    {
        Debug.Log("Special");
    }
    
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
