using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System;

using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    
    [SerializeField] [Range(0, 1)] private float cooldown;
    private float cooldownTimer;
    
    [SerializeField] [Min(1)] private float shapeshiftInterval = 10;
    private float shapeshiftTimer;
    
    private Animator animator;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    [SerializeField] private InputActionAsset actionAsset;
    private InputActionMap actionMap;
    
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction specialAction;

    private BuildSystem weaponSystem;
    private Build currentBuild;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
        weaponSystem = GetComponent<BuildSystem>();
        
        shapeshiftTimer = shapeshiftInterval;
        currentBuild = weaponSystem.Builds[0];
        
        sr.sprite = currentBuild.GetSprite();
        movementSpeed = currentBuild.GetMoveSpeed();
        cooldown = currentBuild.GetCooldown();
        
        actionMap = actionAsset.FindActionMap("Player");
        actionMap.Enable();
        
        moveAction = actionMap.FindAction("Move");
        attackAction = actionMap.FindAction("Attack");
        specialAction = actionMap.FindAction("Special");
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
            currentBuild?.Shoot(new Vector2(rb.linearVelocity.x, 0));
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
        
        int i = random.Next(weaponSystem.Builds.Length);
        currentBuild = weaponSystem.Builds[i];
        
        //
        animator.SetInteger("BuildIndex", i);
        sr.sprite = currentBuild.GetSprite();
        movementSpeed = currentBuild.GetMoveSpeed();
        cooldown = currentBuild.GetCooldown();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy") GetHit();
    }

    public void GetHit()
    {
        Flash();
        GameData.PlayerHp -= 1;

        if (GameData.PlayerHp <= 0) GameManager.GameOver();
    }
    
    void Flash()
    {
        sr.color = Color.red;
        sr.DOColor(Color.white, 0.3f).SetEase(Ease.InExpo);
    }
}
