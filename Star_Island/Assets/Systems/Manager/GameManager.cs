using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }

    // 임시
    [Header("UI")]
    [SerializeField]
    private GameObject gameStartUI;
    [SerializeField]
    private GameObject gameOverUI;

    private bool gameStarted = false;
    private bool gameOver = false;
    private int curScore = 0;
    private int highScore = 0;
    private float distOfSun = 0;

    public bool GameStarted => gameStarted;
    public bool GameOver => gameOver;
    public int CurScore => curScore;
    public int HighScore => highScore;

    private void Awake()
    {
        InitSingleton();
        Time.timeScale = 0;
    }
    private void InitSingleton()
    {
        if (Inst != null)
        {
            Destroy(gameObject);
            return;
        }

        Inst = this;
    }
    private void Update()
    {
        if (!gameOver && !gameStarted)
        {
            gameStartUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                gameStartUI.SetActive(false);
                StartGame();
            }

            return;
        }

        if (gameOver)
        {
            gameOverUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                gameOverUI.SetActive(false);
                RestartGame();
            }
        }
    }


    public void StartGame()
    {
        // Reset Current Score
        curScore = 0;

        gameOver = false;
        gameStarted = true;

        // Resume Time
        Time.timeScale = 1;
    }
    public void EndGame()
    {
        // Update HighScore
        highScore = highScore < curScore ? curScore : highScore;

        gameOver = true;

        // Stop Time
        Time.timeScale = 0;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOver = false;
        gameStarted = false;
    }
}
