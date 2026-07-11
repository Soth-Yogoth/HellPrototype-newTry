using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class FinalBoss : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject body;
    [SerializeField][Min(0)] private float timeToEnd;
    private Tween lightTween;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
        transform.DOScale(new Vector3(15f, 15f, 15f), timeToEnd).SetEase(Ease.Linear);
        
        Light2D light = GetComponent<Light2D>();
        lightTween = DOTween.To(()=> light.pointLightOuterRadius, 
            x => light.pointLightOuterRadius = x, 30, timeToEnd).SetEase(Ease.InQuart);
    }

    void OnDestroy()
    {
        transform.DOKill();
        lightTween?.Kill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Вы покидаете колесо Сансары. Вы уверены, что хотите продолжить?");
            GameData.OnEnterToNirvana?.Invoke();
        }
    }

    // private void StartLightning()
    // {
    //     transform.DOScale(new Vector3(15f, 15f, 15f), timeToEnd).SetEase(Ease.Linear);
    //     
    //     //Light2D light = GetComponent<Light2D>();
    //     lightTween = DOTween.To(()=> light.pointLightOuterRadius, 
    //         x => light.pointLightOuterRadius = x, 30, timeToEnd).SetEase(Ease.InQuart);
    // }
}
