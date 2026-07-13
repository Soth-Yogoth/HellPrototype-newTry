using System;
using UnityEngine;
using DG.Tweening;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    
    private Sprite[] heartSprites;
    
    void Start()
    {
        Debug.Log("[PlayerHP] Start");
        
        UpdateHearts(GameData.PlayerHp);
        GameData.OnPlayerHpChanged += UpdateHearts;
    }

    void UpdateHearts(int heartsQuantity)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < heartsQuantity; i++)
        {
            GameObject hp = Instantiate(heartPrefab, transform);
            
            hp.transform.DOLocalMoveY(heartPrefab.transform.localPosition.y + 0.1f, 0.2f)
                .SetEase(Ease.OutSine)
                .OnComplete(() => 
                    hp.transform.DOLocalMoveY(heartPrefab.transform.localPosition.y - 0.1f, 0.2f).SetEase(Ease.InQuad));
        }
    }

    private void OnDestroy()
    {
        GameData.OnPlayerHpChanged -= UpdateHearts;
    }
}
