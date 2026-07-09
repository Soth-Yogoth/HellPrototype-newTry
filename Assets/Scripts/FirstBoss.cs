using UnityEngine;
using UnityEngine.Serialization;

enum State
{
    Base,
    Special,
    BaseCooldown,
    SpecialCooldown,
}

public class FirstBoss : BaseEnemy
{
    [SerializeField] [Min(0)] private float specialDuration;
    [SerializeField] [Min(0)] private float baseDuration;
    [SerializeField] [Min(0)] private float cooldown;
    private float cooldownTimer;

    private State currentState = State.Base;
    
    [SerializeField] [Min(0)] private float movementBorder = 5; //Решение временное
    private Vector2 targetAncor;
    private bool isMovingToRight = true;

    [SerializeField] protected GameObject bulletSpawner;
    [SerializeField] private GameObject specialAttack;

    private Rigidbody2D rb;

    void Start()
    {
        cooldownTimer = specialDuration;
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        
        Debug.Log(currentState + ", timer: " + cooldownTimer);
        
        if (cooldownTimer <= 0)
        {
            switch (currentState)
            {
                case State.Base:
                    currentState = State.BaseCooldown;
                    bulletSpawner.gameObject.SetActive(false);
                    cooldownTimer = cooldown;
                    break;
                case State.Special:
                    currentState = State.SpecialCooldown;
                    specialAttack.gameObject.SetActive(false);
                    cooldownTimer = cooldown;
                    break;
                case State.BaseCooldown:
                    currentState = State.Special;
                    specialAttack.gameObject.SetActive(true);
                    cooldownTimer = specialDuration;
                    break;
                case State.SpecialCooldown:
                    currentState = State.Base;
                    bulletSpawner.gameObject.SetActive(true);
                    cooldownTimer = baseDuration;
                    break;
            }
        }
        
        if (currentState == State.Base) Move();
        else rb.linearVelocity = Vector2.zero;
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
