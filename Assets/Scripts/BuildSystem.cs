using System;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Build 
{
    [Header("Character")]
    //[SerializeField] private Color spriteColor;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float moveSpeed;

    [Header("Gun")] 
    //[SerializeField] private BulletPool Pool;
    [SerializeField][Min(1)] private int bulletCount;
    [SerializeField][Range(0, 1)]  private float cooldown;
    [SerializeField] private float arcAngle;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float impulseFactor;

    public BulletPool Pool { get; set; }
    public Transform HostTransform { get; set; }
    
    public void Shoot(Vector2 impulse)
    {
        float stepAngle = bulletCount > 1 ? arcAngle / (bulletCount - 1) : 90;
        
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

    //Ленивое решение 2
    public Sprite GetSprite()
    {
        return sprite;
    }
    
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    
    public float GetCooldown()
    {
        return cooldown;
    }
}

public class BuildSystem : MonoBehaviour
{
    private BulletPool pool;
    
    [SerializeField] private Build[] builds;
    public Build[] Builds
    {
        get { return builds; }
    }
    
    void Start()
    {
        pool = GetComponent<BulletPool>();

        foreach (Build build in builds)
        {
            build.Pool = pool;
            build.HostTransform = transform;
        }
    }
}
