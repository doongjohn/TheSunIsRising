using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AScoreMananger : MonoBehaviour
{
    [SerializeField] bool bScoreUpdate;

    public bool IsScoreUpdate => bScoreUpdate;
    public int Score{ get => AGameMananger.inst.ThisGameScore; set => AGameMananger.inst.ThisGameScore = value; }

    void Update()
    {
        if(IsScoreUpdate)
        {
            Score = (int)(AGameMananger.inst.PlayerPos.x * 1000);
        }
    }

    void Reset()
    {
        Score = 0;
    }
}
