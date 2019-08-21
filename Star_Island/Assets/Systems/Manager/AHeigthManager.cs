using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AHeigthManager : MonoBehaviour
{
    [SerializeField] int maxHeigth;
    [SerializeField] int minHeigth;

    public int Heigth => (int)AGameMananger.Inst.IslandAndPlayerPos.y;
    public int Max { get => maxHeigth; set => maxHeigth = value; }
    public int Min { get => minHeigth; set => minHeigth = value; }

    public bool IsOverRange => (Heigth > maxHeigth) || (Heigth < minHeigth);
}
