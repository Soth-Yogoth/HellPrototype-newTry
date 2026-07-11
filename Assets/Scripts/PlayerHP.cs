using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    
    private Sprite[] heartSprites;
    
    void Start()
    {
        UpdateHearts(GameData.PlayerHp);
        GameData.OnPlayerHpChanged +=  UpdateHearts;
    }

    void UpdateHearts(int heartsQuantity)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < heartsQuantity; i++)
        {
            Instantiate(heartPrefab, transform);
        }
    }
}
