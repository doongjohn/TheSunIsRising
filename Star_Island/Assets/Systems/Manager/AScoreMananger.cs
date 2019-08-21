using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AScoreMananger : MonoBehaviour
{
    public int Score{ get => AGameMananger.Inst.CurrentGameScore; set => AGameMananger.Inst.CurrentGameScore = value; }

    void Update()
    {
        Score = (int)(AGameMananger.Inst.IslandAndPlayerPos.x * 1000);
    }

    void Reset()
    {
        Score = 0;
    }
}
