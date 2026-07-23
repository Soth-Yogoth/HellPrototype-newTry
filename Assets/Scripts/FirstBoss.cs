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
    
    private Animator animator;
    private AudioSource audioSource;
    
    [SerializeField] [Min(0)] private float movementBorder = 5; //Решение временное
    private Vector2 targetAncor;
    private bool isMovingToRight = true;
    private Vector2 direction = Vector2.down;

    //[SerializeField] protected GameObject bulletSpawner;
    [SerializeField] private GameObject specialAttack;
    [SerializeField] private HpBar hpBar;

    private Rigidbody2D rb;

    private int difficulty = 0;

    void Start()
    {
        cooldownTimer = baseDuration;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        rb.linearVelocity = Vector2.down * moveSpeed;
    }
    
    void Update()
    {
        if (!isReady) return;
        
        cooldownTimer -= Time.deltaTime;
        
        specialAttack.transform.rotation = Quaternion.Euler(0, 0, 255 + getAngle());
        
        if (cooldownTimer <= 0)
        {
            switch (currentState)
            {
                case State.Base:
                    currentState = State.BaseCooldown;
                    bulletSpawner.gameObject.SetActive(false);
                    animator.SetBool("Special", true);
                    cooldownTimer = animator.GetCurrentAnimatorClipInfo(0).Length;
                    break;
                case State.Special:
                    currentState = State.SpecialCooldown;
                    specialAttack.gameObject.SetActive(false);
                    animator.SetBool("Special", false);
                    cooldownTimer = cooldown;
                    break;
                case State.BaseCooldown:
                    currentState = State.Special;
                    specialAttack.gameObject.SetActive(true);
                    audioSource.Play();
                    cooldownTimer = specialDuration;
                    break;
                case State.SpecialCooldown:
                    currentState = State.Base;
                    bulletSpawner.gameObject.SetActive(true);
                    cooldownTimer = baseDuration;
                    MakeHarder();
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
        
        direction = isMovingToRight ? Vector2.right : Vector2.left;
        direction.Normalize();
        
        rb.linearVelocity = direction * moveSpeed;
    }
    
    public override void TakeDamage(int damage)
    {
        if(!isReady) return;
        
        if (health - damage <= 0) Death();
        else health -= damage;
        
        hpBar.UpdateHpBar(health, maxHp);
    }

    private float getAngle()
    {
        Vector2 direction = playerTransform.position - transform.position;
        float angle = Vector2.Angle(Vector2.down, direction);
        
        return direction.x > 0 ? angle : -angle;
    }

    protected new void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BossFinalLine")
        {
            bulletSpawner.SetActive(true);
            isReady = true;
        }
    }

    private void MakeHarder()
    {
        if (difficulty == 3) return;
        
        BaseBulletSpawner spawner = bulletSpawner.GetComponent<BaseBulletSpawner>();

        spawner.bulletQuantityPerShot += 4;
        spawner.fireInterval -= difficulty / 10f;
        spawner.bulletSpeed += difficulty;
        
        difficulty++;
    }
}
