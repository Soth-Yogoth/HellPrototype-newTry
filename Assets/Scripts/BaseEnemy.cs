using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected GameObject bulletSpawner;
    
    [SerializeField] [Min(1)] protected int maxHp = 150;
    [SerializeField] [Min(1)] protected int moveSpeed;
    
    protected int health;
    
    protected SpriteRenderer sr;
    
    protected static Transform playerTransform;
    public static Transform PlayerTransform { set { playerTransform = value; } } //Вызов из GameManager

    protected void Start()
    {
        health = maxHp;
        
        bulletSpawner.gameObject.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
    }
    
    public virtual void TakeDamage(int damage)
    {
        if (health - damage <= 0) Death();
        else
        {
            health -= damage;
            
            Squish();
            Flash();
        }
    }
    
    void Flash()
    {
        sr.DOComplete();
        
        sr.color = Color.red;
        sr.DOColor(Color.white, 0.3f).SetEase(Ease.InExpo);
    }
    
    void Squish()
    {
        transform.DOComplete();
        
        Vector3 originScale = transform.localScale;
        transform.DOScale(new Vector3(originScale.x * 1.1f, originScale.y * 0.8f, 1), 0.3f)
            .OnComplete(() => transform.DOScale(originScale, 0.3f));
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }
}
