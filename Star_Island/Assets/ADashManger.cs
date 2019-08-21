using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADashManger : MonoBehaviour
{
    [SerializeField] float dashTime;
    float dashTimer = 0;
    [SerializeField] int dashPower;

    [SerializeField] float dashGage = 100;
    bool bDashProcess = false;
    public bool IsDash 
    {
        get 
        { 
            if(dashGage >= AGameMananger.inst.StarManager.MaxStarGage)
            {
                return true; 
            }
            return bDashProcess;
        }
    }

    internal void AddGage(int gage)
    {
        dashGage += gage;
    }

    public float DashGage => dashGage;

    public int DashPower {
        get { 
            return IsDash ? dashPower : 0; 
        }
        set => dashPower = value; }


    private void Update() 
    {
        if (IsDash)
        {
            bDashProcess = true;
            dashTimer += Time.deltaTime;
            if(dashTimer > dashTime)
            {
                dashTimer = 0;
                bDashProcess = false;
            }
            dashGage = Mathf.Max(dashGage - ((AGameMananger.inst.StarManager.MaxStarGage / dashTime)*Time.deltaTime), 0);
        }
    }
}
