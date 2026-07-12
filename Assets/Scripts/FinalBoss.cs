using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class FinalBoss : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private Light2D light;
    [SerializeField][Min(0)] private float timeToEnd;
    
    private Tween lightTween;
    private Tween colliderTween;

    private Rigidbody2D rb;
    private CircleCollider2D collider;
    private bool isReady = false;
    
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.down;
    }

    void OnDestroy()
    {
        transform.DOKill();
        lightTween?.Kill();
        colliderTween?.Kill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BossFinalLine")
        {
            isReady = true;
            rb.linearVelocity = Vector2.zero;
            
            StartLightning();
            GameScreen.PushBorders(timeToEnd);
        }
        if (collision.tag == "Player")
        {
            GameData.OnEnterToNirvana?.Invoke();
        }
    }

    private void StartLightning()
    {
        colliderTween = DOTween.To(() => collider.radius, 
            x => collider.radius = x, 15, timeToEnd).SetEase(Ease.InQuart);
        
        lightTween = DOTween.To(()=> light.pointLightOuterRadius, 
            x => light.pointLightOuterRadius = x, 30, timeToEnd).SetEase(Ease.InQuart);
    }
}
