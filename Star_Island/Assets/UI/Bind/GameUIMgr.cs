using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIMgr : AUIRoot
{
    AText mScore;
    AText mHeight;
    AText mSunDist;
    AText mBestScore;

    ASlider sHeight;
    ASlider sGauge;

    public void OnGaugeSliderChanage()
    {
        Debug.Log("Gauge Chanage");
    }

    public void OnHeightSliderChanage()
    {
        Debug.Log("Height Chanage");
    }


    private void Start() 
    {
        sHeight.Min = AGameMananger.Inst.HeigthManager.Min;
        sHeight.Max = AGameMananger.Inst.HeigthManager.Max;
        sGauge.Min = 0;
        sGauge.Max = AGameMananger.Inst.StarManager.MaxStarGauge;
        sGauge.Value = AGameMananger.Inst.DashMgr.DashGauge;
    }

    private void Update() 
    {
        mBestScore.MText = "Best Score: " + AGameMananger.Inst.BestScore.ToString();
        mScore.MText = "Score: " + AGameMananger.Inst.CurrentGameScore.ToString();
        sHeight.Value = AGameMananger.Inst.HeigthManager.Heigth;
        mHeight.MText = AGameMananger.Inst.HeigthManager.Heigth.ToString() + "M";
        mSunDist.MText = Mathf.RoundToInt(AGameMananger.Inst.SunManager.DistOfSun).ToString() + "M";
        sGauge.Value = AGameMananger.Inst.DashMgr.DashGauge;
    }
}
