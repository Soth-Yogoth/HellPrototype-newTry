using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System;

using Random = System.Random;

// enum Shape
// {
//     Human,
//     Octopus,
//     Puffer
// }

delegate void Shoot(float bulletSpeed, Vector2 momentum);

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
    
    //private Shape playerShape = Shape.Human;
    
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    [SerializeField] private InputActionAsset actionAsset;
    private InputActionMap actionMap;
    
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction specialAction;

    private WeaponSystem weaponSystem;

    private Gun gun;
    // private Shoot[] shoots;
    // private Shoot makeShoot;
    
    private BulletPool pool;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pool = GetComponent<BulletPool>();
        
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
        weaponSystem = GetComponent<WeaponSystem>();
        
        Debug.Log(weaponSystem);
        Debug.Log(weaponSystem.Guns.Length);
        
        gun = weaponSystem.Guns[0];

        // shoots = new Shoot[]
        // {
        //     weaponSystem.BaseAttack,
        //     weaponSystem.OctopusAttack,
        // };
        // makeShoot = shoots[1];
        
        actionMap = actionAsset.FindActionMap("Player");
        actionMap.Enable();
        
        moveAction = actionMap.FindAction("Move");
        attackAction = actionMap.FindAction("Attack");
        specialAction = actionMap.FindAction("Special");
        
        //BaseEnemy.PlayerTransform = transform;
        //GameManager.player = this;
        
        shapeshiftTimer = shapeshiftInterval;

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
        if(cooldownTimer < 0 && attackAction.inProgress)
        {
            //makeShoot(bulletSpeed, (Vector2)rb.linearVelocity * momentum);
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
        //Array shapes = Enum.GetValues(typeof(Shape));
        Random random = new Random();
        //playerShape = (Shape)random.Next(shapes.Length);
        
        int i = random.Next(weaponSystem.Guns.Length);
        gun = weaponSystem.Guns[i];

        // switch (playerShape)
        // {
        //     case Shape.Human:
        //         sr.color = Color.black;
        //         gun = weaponSystem.getGun(0);
        //         break;
        //     case Shape.Octopus:
        //         sr.color = Color.green;
        //         gun = weaponSystem.getGun(1);
        //         break;
        //     case Shape.Puffer:
        //         sr.color = Color.yellow;
        //         gun = weaponSystem.getGun(2);
        //         break;
    //}
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
