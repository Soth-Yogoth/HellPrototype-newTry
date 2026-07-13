using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    
    [SerializeField] private int capacity = 100;
    [SerializeField] private int maxSize = 1000;

    ObjectPool<GameObject> pool;
    
    public GameObject GetBullet() => pool.Get();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pool = new ObjectPool<GameObject>(OnCreatePooledObject, OnGetFromPool, OnReleaseFromPool,
            OnDestroyPulledObject, true, capacity, maxSize);
    }
    
    GameObject OnCreatePooledObject()
    {
        GameObject bullet = Instantiate(bulletPrefab, Vector2.zero, Quaternion.identity);
        bullet.SetActive(false);
        
        Projectail projectail = bullet.GetComponent<Projectail>();
        projectail.ObjectPool = pool;
        
        projectail.SetHostType(gameObject.tag);
        
        return bullet;
    }
    
    void OnGetFromPool(GameObject bullet)
    {
        Projectail projectail = bullet.GetComponent<Projectail>();
        projectail.gameObject.SetActive(true);
        
        projectail.IsReleased = false;
    }

    void OnReleaseFromPool(GameObject bullet)
    {
        Projectail projectail = bullet.GetComponent<Projectail>();
        projectail.gameObject.SetActive(false);
    }

    void OnDestroyPulledObject(GameObject bullet)
    {
        
    }
}
