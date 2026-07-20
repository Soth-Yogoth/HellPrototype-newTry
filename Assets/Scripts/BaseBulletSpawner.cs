using UnityEngine;

public abstract class BaseBulletSpawner : MonoBehaviour
{
    protected BulletPool pool;
    
    [SerializeField] public float fireInterval = 1f;
    private float timer = 0f;
    
    [SerializeField] [Range(1, 100)] public int bulletQuantityPerShot;
    [SerializeField] [Range(1, 100)] public float bulletSpeed = 1f;

    public bool Enabled = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {  
        pool = GetComponent<BulletPool>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!Enabled) return;
        
        timer += Time.deltaTime;
        
        if (timer >= fireInterval)
        {
            timer = 0f;
            Shoot();
        }
    }

    protected virtual void Shoot() {}
}
