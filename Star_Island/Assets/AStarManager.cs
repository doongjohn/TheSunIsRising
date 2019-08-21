using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager : MonoBehaviour
{
    [SerializeField] float maxStarGage;

    public float MaxStarGage { get => maxStarGage; set => maxStarGage = value; }

    public void AddStarGage(int gage)
    {
        AGameMananger.inst.DashMgr.AddGage(gage);
    }
}
