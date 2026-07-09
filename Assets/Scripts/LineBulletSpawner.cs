using UnityEngine;

public class LineBulletSpawner : BaseBulletSpawner
{
    private Vector2[] spawnPoints;

    [SerializeField] [Min(0)]  protected float size;
    [SerializeField] protected Vector2 direction = Vector2.right;

    [SerializeField] [Range(0, 360)] public float directionAngle;

    protected void OnValidate()
    {
        direction.x = Mathf.Cos(directionAngle * Mathf.Deg2Rad);
        direction.y = Mathf.Sin(directionAngle * Mathf.Deg2Rad);
    }

    protected override void Shoot()
    {
        spawnPoints = GetSpawnPoints();
        
        foreach (Vector2 point in spawnPoints)
        {
            GameObject bullet = pool.GetBullet();
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            
            bullet.transform.position = point;
            bulletRigidbody.linearVelocity = direction * bulletSpeed;
        }
    }
    
    protected Vector2[] GetSpawnPoints()
    {
        Vector2[] points = new Vector2[bulletQuantityPerShot];
        
        Vector2 firstPoint = transform.position - transform.right * size * 0.5f;
        Vector2 secondPoint = transform.position + transform.right * size * 0.5f;
        
        float stepX = (secondPoint.x - firstPoint.x) / (bulletQuantityPerShot - 1);
        float stepY = (secondPoint.y - firstPoint.y) / (bulletQuantityPerShot - 1);
        
        for (int i = 0; i < bulletQuantityPerShot; i++)
        {
            float x = firstPoint.x + stepX * i;
            float y = firstPoint.y + stepY * i;

            points[i] = new Vector2(x, y);
        }
        points[bulletQuantityPerShot - 1] = secondPoint;
        
        return points;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        
        Vector2 start = transform.position - transform.right * size * 0.5f;
        Vector2 end = transform.position + transform.right * size * 0.5f;
        
        Gizmos.DrawLine(start, end);
        Gizmos.DrawRay(transform.position, direction); 
    }
}
