using System;
using UnityEngine;

public class ArcBulletSpawner : BaseBulletSpawner
{
    [SerializeField] [Range(0, 360)] private float arcAngle = 360;
    
    protected override void Shoot()
    {
        float stepAngle = arcAngle / bulletQuantityPerShot;

        for (int i = 0; i < bulletQuantityPerShot; i++)
        {
            GameObject bullet = pool.GetBullet();
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            
            float spawnPoint = (stepAngle * i + transform.rotation.eulerAngles.z) * Mathf.Deg2Rad;
            Vector2 bulletVelocity = new Vector2((float)Math.Cos(spawnPoint), (float)Math.Sin(spawnPoint));
            
            bullet.transform.position = transform.position;
            bulletRigidbody.linearVelocity = bulletVelocity * bulletSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
