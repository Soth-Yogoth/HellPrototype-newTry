using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform screenBounds;
    
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private GameObject Player;
    
    [SerializeField] private GameObject HpBar;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject GameOverPanel;

    private static GameObject[] enemies;
    private static GameObject player;
    
    private static GameObject winPanel;
    private static GameObject gameOverPanel;
    
    private static GameObject baseScene;
    
    public static Vector2 FirstScreenCorner;
    public static Vector2 SecondScreenCorner;
    
    void Start()
    {
        Time.timeScale = 0;
        if (SceneManager.GetSceneByName("MainMenu").isLoaded) SceneManager.UnloadSceneAsync("MainMenu");
        
        enemies = Enemies;

        player = Player;
        
        GameData.OnStageUpdate += OnStageUpdate;
        GameData.OnGameOver += GameOver;
        GameData.OnEnterToNirvana += TrueEnding;
        
        FirstScreenCorner = transform.position - (screenBounds.localScale / 2);
        SecondScreenCorner = transform.position + (screenBounds.localScale / 2);

        BaseEnemy.PlayerTransform = player.transform;
        
        winPanel = WinPanel;
        gameOverPanel = GameOverPanel;
        
        Instantiate(enemies[0]);
    }

    public static void Play()
    {
        Time.timeScale = 1;
    }

    private void OnStageUpdate(int level)
    {
        GameData.PlayerHp += GameData.PlayerHp < 4 ? 1 : 0;
        Instantiate(enemies[level]);
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
        GameData.OnStageUpdate -= OnStageUpdate;
        GameData.OnGameOver -= GameOver;
        GameData.OnEnterToNirvana -= TrueEnding;
    }
}
