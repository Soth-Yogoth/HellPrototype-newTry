using System;
using UnityEngine;

[System.Serializable]
public class Gun 
{
    [SerializeField] private SpriteRenderer sprite;
    
    [SerializeField][Min(1)] private int bulletCount;
    [SerializeField][Min(0)] private float fireRate;
    [SerializeField] private float arcAngle;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float impulseFactor;

    public BulletPool Pool { get; set; }
    public Transform HostTransform { get; set; }
    
    public void Shoot(Vector2 impulse)
    {
        float stepAngle = arcAngle / bulletCount;
        
        for(int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Pool.GetBullet();
            bullet.transform.position = HostTransform.position;

            float offsetAngle = (180 - arcAngle) / 2;
            float spawnPoint = (stepAngle * i + offsetAngle) * Mathf.Deg2Rad;
            Vector2 bulletVelocity = new Vector2((float)Math.Cos(spawnPoint), (float)Math.Sin(spawnPoint));
            
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = bulletVelocity * bulletSpeed + impulse * impulseFactor;
        }
    }
}

public class WeaponSystem : MonoBehaviour
{
    private BulletPool pool;
    
    [SerializeField] private Gun[] guns;
    public Gun[] Guns
    {
        get { return guns; }
    }
    
    void Start()
    {
        pool = GetComponent<BulletPool>();

        foreach (Gun gun in guns)
        {
            gun.Pool = pool;
            gun.HostTransform = transform;
        }
    }
}
