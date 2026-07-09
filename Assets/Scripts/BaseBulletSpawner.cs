using UnityEngine;

public abstract class BaseBulletSpawner : MonoBehaviour
{
    protected BulletPool pool;
    
    [SerializeField] private float fireInterval = 1f;
    private float timer = 0f;
    
    [SerializeField] [Range(1, 100)] protected int bulletQuantityPerShot;
    [SerializeField] [Range(1, 100)] protected float bulletSpeed = 1f;
    [SerializeField] protected float delay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {  
        pool = GetComponent<BulletPool>();
    }

    // Update is called once per frame
    protected void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= fireInterval)
        {
            timer = 0f;
            Shoot();
        }
    }

    protected virtual void Shoot() {}
}
