using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System;

using Random = System.Random;

enum Shape
{
    Human,
    Octopus,
    Puffer
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool godMod = false;
    
    private int hp = 150;
    public event Action OnPlayerDeath;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] [Range(-1, 1)] private float momentum;
    
    [SerializeField] [Range(0, 1)] private float cooldown;
    private float cooldownTimer;
    
    private float shapeshiftInterval = 10;
    private float shapeshiftTimer;
    
    private Shape playerShape = Shape.Human;
    
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    [SerializeField] private InputActionAsset actionAsset;
    private InputActionMap actionMap;
    
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction specialAction;

    private BulletPool pool;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pool = GetComponent<BulletPool>();
        
        actionMap = actionAsset.FindActionMap("Player");
        actionMap.Enable();
        
        moveAction = actionMap.FindAction("Move");
        attackAction = actionMap.FindAction("Attack");
        specialAction = actionMap.FindAction("Special");
        
        BaseEnemy.PlayerTransform = transform;
        GameManager.player = this;
        
        shapeshiftTimer = shapeshiftInterval;
        
        sr = GetComponent<SpriteRenderer>();

        if (godMod) hp = 1500;
    }

    // Update is called once per frame
    void Update()
    {
        if (shapeshiftTimer <= 0)
        {
            Shapeshift();

            if (shapeshiftInterval > 5) shapeshiftInterval -= 2;
            shapeshiftTimer = shapeshiftInterval;
        }
        
        shapeshiftTimer -= Time.deltaTime;
        
        Move();
        if(cooldownTimer < 0)
        {
            Attack();
        }
        else
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    void OnDestroy()
    {
        actionMap.Disable();
    }

    private void Move()
    {
        Vector2 moveDirection = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = moveDirection * movementSpeed;
    }

    private void Attack()
    {
        if (attackAction.inProgress)
        {
            GameObject bullet = pool.GetBullet();
            bullet.transform.position = transform.position;
            
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = Vector2.up * bulletSpeed + rb.linearVelocity * momentum;
            
            cooldownTimer = cooldown;
        }
    }

    private void Shapeshift()
    {
        Array shapes = Enum.GetValues(typeof(Shape));
        Random random = new Random();
        playerShape = (Shape)random.Next(shapes.Length);

        switch (playerShape)
        {
            case Shape.Human:
                sr.color = Color.black;
                break;
            case Shape.Octopus:
                sr.color = Color.blueViolet;
                break;
            case Shape.Puffer:
                sr.color = Color.yellow;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy") GetHit(hp);
    }

    public void GetHit(int damage)
    {
        Flash(sr.color);
        hp -= damage;
        
        Debug.Log(hp);

        //if (hp <= 0) OnPlayerDeath?.Invoke();
        if (hp <= 0) GameManager.GameOver();
    }
    
    void Flash(Color color)
    {
        sr.color = Color.red;
        sr.DOColor(color, 0.3f).SetEase(Ease.InExpo);
    }
}
