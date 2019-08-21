using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    [Header("Sun")]
    [SerializeField]
    private Transform sun;
    [SerializeField]
    private float minSunDist = 0;
    private float distOfSun = 0;

    public float DistOfSun => distOfSun;
    public bool IsSunTooClose => distOfSun <= minSunDist;

    public void UpdateDistOfSun()
    {
        distOfSun = Player.Inst.transform.position.x - sun.position.x;
    }
}
