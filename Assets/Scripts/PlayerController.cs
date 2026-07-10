using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System;

using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool godMod = false;
    
    private int hp = 150;
    public event Action OnPlayerDeath;
    
    [SerializeField] private float movementSpeed;
    
    [SerializeField] [Range(0, 1)] private float cooldown;
    private float cooldownTimer;
    
    [SerializeField] [Min(1)] private float shapeshiftInterval = 10;
    private float shapeshiftTimer;
    
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    [SerializeField] private InputActionAsset actionAsset;
    private InputActionMap actionMap;
    
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction specialAction;

    private WeaponSystem weaponSystem;
    private Gun gun;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
        weaponSystem = GetComponent<WeaponSystem>();
        
        Debug.Log(weaponSystem);
        Debug.Log(weaponSystem.Guns.Length);
        
        gun = weaponSystem.Guns[0];
        
        actionMap = actionAsset.FindActionMap("Player");
        actionMap.Enable();
        
        moveAction = actionMap.FindAction("Move");
        attackAction = actionMap.FindAction("Attack");
        specialAction = actionMap.FindAction("Special");
        
        shapeshiftTimer = shapeshiftInterval;

        if (godMod) hp = 1500;
    }
    
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
        if(cooldownTimer < 0 && attackAction.inProgress)
        {
            gun?.Shoot(rb.linearVelocity);
            cooldownTimer = cooldown;
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

    private void Shapeshift()
    {
        Random random = new Random();
        
        int i = random.Next(weaponSystem.Guns.Length);
        gun = weaponSystem.Guns[i];
        
        sr.color = gun.GetSpriteColor();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy") GetHit(hp);
    }

    public void GetHit(int damage)
    {
        Flash(sr.color);
        hp -= damage;

        if (hp <= 0) GameManager.GameOver();
    }
    
    void Flash(Color color)
    {
        sr.color = Color.red;
        sr.DOColor(color, 0.3f).SetEase(Ease.InExpo);
    }
}
