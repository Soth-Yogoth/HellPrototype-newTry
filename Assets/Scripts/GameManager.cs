using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform screenBounds;
    
    [SerializeField] private GameObject[] Enemies;
    
    // [SerializeField] private GameObject firstWave;
    // [SerializeField] private GameObject secondWave;
    //
    // [SerializeField] private GameObject firstBossPrefab;
    // [SerializeField] private GameObject secondBossPrefab;
    
    [SerializeField] private GameObject Player;
    
    [SerializeField] private GameObject HpBar;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject GameOverPanel;
    //[SerializeField] private GameObject MainMenu;
    
    // private static GameObject[] enemyWaves;
    // private static GameObject[] bosses;

    private static GameObject[] enemies;

    private static GameObject player;
    
    private static GameObject hpBar;
    private static GameObject winPanel;
    private static GameObject gameOverPanel;
    
    private static GameObject baseScene;
    
    public static Vector2 FirstScreenCorner;
    public static Vector2 SecondScreenCorner;

    private static int level = 0;
    
    void Start()
    {
        Time.timeScale = 0;
        if (SceneManager.GetSceneByName("MainMenu").isLoaded) SceneManager.UnloadSceneAsync("MainMenu");
        
        // bosses = new GameObject[] { firstBossPrefab, secondBossPrefab };
        // enemyWaves = new GameObject[] { firstWave, secondWave };
        
        enemies = Enemies;

        player = Player;
        
        GameData.OnAllMobsDead += OnAllMobsDead;
        //GameData.OnBossKilled += SpawnMobs;
        GameData.OnGameOver += GameOver;
        GameData.OnEnterToNirvana += TrueEnding;
        
        FirstScreenCorner = transform.position - (screenBounds.localScale / 2);
        SecondScreenCorner = transform.position + (screenBounds.localScale / 2);

        BaseEnemy.PlayerTransform = player.transform;

        hpBar = HpBar;
        winPanel = WinPanel;
        gameOverPanel = GameOverPanel;
        
        Instantiate(enemies[0]);
        //SpawnMobs();
    }

    public static void Play()
    {
        Time.timeScale = 1;
    }

    // public static void OnStartGame()
    // {
    //     Instantiate(player);
    //     Instantiate(hpBar);
    //     Instantiate(enemies[0]);
    //     SpawnMobs();
    // }

    // public static void SpawnMobs()
    // {
    //     //GameData.PlayerHp += GameData.PlayerHp < 4 ? 1 : 0;
    //     //Instantiate(enemyWaves[GameData.BossesKilled]);
    //     Instantiate(enemies[level]);
    // }

    private void OnAllMobsDead()
    {
        GameData.PlayerHp += GameData.PlayerHp < 4 ? 1 : 0;
        Instantiate(enemies[level++]);
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
        Application.Quit();
    }

    void OnDestroy()
    {
        GameData.OnAllMobsDead -= OnAllMobsDead;
        //GameData.OnBossKilled -= SpawnMobs;
        GameData.OnGameOver -= GameOver;
        GameData.OnEnterToNirvana -= TrueEnding;
    }
}
