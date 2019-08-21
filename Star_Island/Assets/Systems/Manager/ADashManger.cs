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
            if(dashGage >= AGameMananger.Inst.StarManager.MaxStarGauge)
            {
                return true; 
            }
            return bDashProcess;
        }
    }

    internal void AddGauge(int gage)
    {
        dashGage += gage;
    }

    public float DashGauge => dashGage;

    public int DashPower {
        get =>IsDash ? dashPower : 0; 
        set => dashPower = value;
    }


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
            dashGage = Mathf.Max(dashGage - ((AGameMananger.Inst.StarManager.MaxStarGauge / dashTime)*Time.deltaTime), 0);
        }
    }
}
