using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;
    public static PlayerController player;
    
    void Start()
    {
        GameData.OnAllMobsDead += OnAllMobsDead;
        //player.OnPlayerDeath += GameOver;
    }
    
    void Update()
    {
        
    }

    private void OnAllMobsDead()
    {
        Debug.Log("All Mobs Dead");
        
        GameObject boss = Instantiate(bossPrefab);
        boss.SetActive(true);
    }
    
    public static void GameOver()
    {
        player.gameObject.SetActive(false);
        Debug.Log("Game Over");
        
        Time.timeScale = 0;
    }
}
