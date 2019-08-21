using UnityEngine;

public class AStarManager : MonoBehaviour
{
    [SerializeField] float maxStarGauge;

    public float MaxStarGauge { get => maxStarGauge; set => maxStarGauge = value; }

    public void AddStarGauge(int gauge)
    {
        AGameMananger.Inst.DashMgr.AddGauge(gauge);
    }
}
