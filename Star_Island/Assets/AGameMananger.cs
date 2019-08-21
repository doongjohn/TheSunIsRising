using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AGameMananger : MonoBehaviour
{
    [SerializeField] string InGameScene;
    int thisGameScore;
    int bestScore = 0;

    [SerializeField] Transform playerTr;
    [SerializeField] ADashManger dashMgr;
    [SerializeField] AHeigthManager heigthManager;
    [SerializeField] AStarManager starManager;

    public int ThisGameScore { get => thisGameScore; set => thisGameScore = value; }

    public GameObject Player => playerTr.gameObject;
    public Vector2 PlayerPos => playerTr.position;
    public int DashPower => dashMgr.DashPower;
    public AHeigthManager HeigthManager => heigthManager;
    public AStarManager StarManager => starManager;
    public ADashManger DashMgr => dashMgr;
    public int BestScore { get => bestScore; set => bestScore = value; }

    public static AGameMananger inst;

    private void Awake()
    {
        inst = this;
        if (!PlayerPrefs.HasKey("BestScore")) BestScore = 0;
        BestScore = PlayerPrefs.GetInt("BestScore");
    }

    public void SetNewBest()
    {
        PlayerPrefs.SetInt("BestScore", thisGameScore);
        BestScore = thisGameScore;
    }
    private void Update() 
    {
        if(HeigthManager.IsOverRange)
        {
            if (thisGameScore > bestScore) SetNewBest();
            
            SceneManager.LoadScene(InGameScene);
        }
    }
}
