using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AGameMananger : MonoBehaviour
{
    // Singleton
    public static AGameMananger Inst { get; private set; }

    #region Var - Inspector
    [Header("Player")]
    [SerializeField] Transform islandAndPlayer;
    #endregion

    #region Var - Score
    int curGameScore = 0;
    int bestScore = 0;
    public int CurrentGameScore { get => curGameScore; set => curGameScore = value; }
    public int BestScore { get => bestScore; set => bestScore = value; }
    #endregion

    #region Var - Player
    public GameObject IslandAndPlayer => islandAndPlayer.gameObject;
    public Vector2 IslandAndPlayerPos => islandAndPlayer.position;
    public int DashPower => dashManager.DashPower;
    #endregion

    #region Var - Manager
    ADashManger dashManager;
    AHeigthManager heigthManager;
    AStarManager starManager;
    SunManager sunManager;

    // Other Manager
    public AHeigthManager HeigthManager => heigthManager;
    public AStarManager StarManager => starManager;
    public ADashManger DashMgr => dashManager;
    public SunManager SunManager => sunManager;
    #endregion

    private void Awake()
    {
        // Init Singleton
        Inst = this;

        // Get Managers
        dashManager = GetComponent<ADashManger>();
        heigthManager = GetComponent<AHeigthManager>();
        starManager = GetComponent<AStarManager>();
        sunManager = GetComponent<SunManager>();

        GetBestScore();
    }
    private void Update()
    {
        sunManager.UpdateDistOfSun();
        ShowBestScore();
        //CheckDeath();
    }

    private void GetBestScore()
    {
        // Get Best Score
        if (!PlayerPrefs.HasKey("BestScore")) BestScore = 0;
        BestScore = PlayerPrefs.GetInt("BestScore");
    }
    private void ShowBestScore()
    {
        if (curGameScore > BestScore)
            BestScore = curGameScore;
    }
    public void SaveNewBest()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
    }
    public void CheckDeath()
    {
        if (HeigthManager.IsOverRange || sunManager.IsSunTooClose)
        {
            SaveNewBest();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
