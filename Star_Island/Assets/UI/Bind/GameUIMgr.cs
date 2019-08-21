using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIMgr : AUIRoot
{
    AText mScore;
    AText mHeight;
    AText mSunDir;
    AText bestScore;

    ASlider sHeight;
    ASlider sGage;

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
        sHeight.Min = AGameMananger.inst.HeigthManager.Min;
        sHeight.Max = AGameMananger.inst.HeigthManager.Max;
        sGage.Min = 0;
        sGage.Max = AGameMananger.inst.StarManager.MaxStarGage;
        sGage.Value = AGameMananger.inst.DashMgr.DashGage;
    }

    private void Update() 
    {
        bestScore.MText = AGameMananger.inst.BestScore.ToString();
        mScore.MText = AGameMananger.inst.ThisGameScore.ToString();
        sHeight.Value = AGameMananger.inst.HeigthManager.Heigth;
        mHeight.MText = AGameMananger.inst.HeigthManager.Heigth.ToString();
        sGage.Value = AGameMananger.inst.DashMgr.DashGage;
    }
}
