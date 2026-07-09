using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform screenBounds;
    [SerializeField] private GameObject bossPrefab;
    
    public static PlayerController player;
    
    public static Vector2 FirstScreenCorner;
    public static Vector2 SecondScreenCorner;
    
    void Start()
    {
        GameData.OnAllMobsDead += OnAllMobsDead;
        //player.OnPlayerDeath += GameOver;
        
        FirstScreenCorner = transform.position - (screenBounds.localScale / 2);
        SecondScreenCorner = transform.position + (screenBounds.localScale / 2);
    }
    
    void Update()
    {
        
    }

    private void OnAllMobsDead()
    {
        GameObject boss = Instantiate(bossPrefab);
        boss.SetActive(true);
    }
    
    public static void GameOver()
    {
        player.gameObject.SetActive(false);
        Time.timeScale = 0;
    }
}
