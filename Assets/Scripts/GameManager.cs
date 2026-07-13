using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform screenBounds;
    
    [SerializeField] private GameObject firstWave;
    [SerializeField] private GameObject secondWave;
    
    [SerializeField] private GameObject firstBossPrefab;
    [SerializeField] private GameObject secondBossPrefab;
    
    [SerializeField] private GameObject Player;
    
    [SerializeField] private GameObject HpBar;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject GameOverPanel;
    //[SerializeField] private GameObject MainMenu;
    
    private static GameObject[] enemyWaves;
    private static GameObject[] bosses;

    private static GameObject player;
    
    private static GameObject hpBar;
    private static GameObject winPanel;
    private static GameObject gameOverPanel;
    
    private static GameObject baseScene;
    
    public static Vector2 FirstScreenCorner;
    public static Vector2 SecondScreenCorner;
    
    void Start()
    {
        bosses = new GameObject[] { firstBossPrefab, secondBossPrefab };
        enemyWaves = new GameObject[] { firstWave, secondWave };

        player = Player;
        
        GameData.OnAllMobsDead += OnAllMobsDead;
        GameData.OnBossKilled += SpawnMobs;
        GameData.OnGameOver += GameOver;
        GameData.OnEnterToNirvana += TrueEnding;
        
        FirstScreenCorner = transform.position - (screenBounds.localScale / 2);
        SecondScreenCorner = transform.position + (screenBounds.localScale / 2);

        BaseEnemy.PlayerTransform = player.transform;

        hpBar = HpBar;
        winPanel = WinPanel;
        gameOverPanel = GameOverPanel;
        
        SpawnMobs();
    }

    // public static void OnStartGame()
    // {
    //     Instantiate(player);
    //     Instantiate(hpBar);
    //     SpawnMobs();
    // }

    public static void SpawnMobs()
    {
        Instantiate(enemyWaves[GameData.BossesKilled]);
    }

    private void OnAllMobsDead()
    { 
        Instantiate(bosses[GameData.BossesKilled]);
    }
    
    public static void GameOver()
    {
        Time.timeScale = 0;
        Instantiate(gameOverPanel);
    }
    
    public static void TrueEnding()
    {
        GameData.OnGameOver -= GameOver;
        Instantiate(winPanel);
    }

    public static void Reset()
    {
        Time.timeScale = 1;

        GameData.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public static void QuitGame()
    {
        Debug.Log("Quit");
    }

    void OnDestroy()
    {
        GameData.OnAllMobsDead -= OnAllMobsDead;
        GameData.OnBossKilled -= SpawnMobs;
        GameData.OnGameOver -= GameOver;
        GameData.OnEnterToNirvana -= TrueEnding;
    }
}
