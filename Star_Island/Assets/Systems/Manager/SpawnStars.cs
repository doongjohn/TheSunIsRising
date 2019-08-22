using System.Collections.Generic;
using UnityEngine;

public class SpawnStars : MonoBehaviour
{
    [SerializeField]
    private float maxY, minY;
    [SerializeField]
    private float minXDist;
    [SerializeField]
    private int divCountY;
    [SerializeField]
    private int minStarCountInColumn;

    [SerializeField]
    private GameObject starPrefab;

    private float? prevSpawnX = null;

    private List<float> posY = new List<float>();
    private List<float> selectPosY = new List<float>();

    private void Awake()
    {
        float dist = maxY - minY;
        float interval = dist / divCountY;

        for (int i = 0; i < divCountY; i++)
            posY.Add(minY + interval * i);
    }
    private void Update()
    {
        SpawnColumnOfStars();
    }

    private void SpawnColumnOfStars()
    {
        if (prevSpawnX == null || transform.position.x - prevSpawnX >= minXDist)
        {
            prevSpawnX = transform.position.x;
            selectPosY = new List<float>(posY);

            for (int i = 0; i < Random.Range(minStarCountInColumn, divCountY); i++)
            {
                int randomIndex = Random.Range(0, selectPosY.Count);
                float randomY = selectPosY[randomIndex];
                selectPosY.RemoveAt(randomIndex);

                GameObject starObj = Instantiate(starPrefab);

                Vector2 starPos = starObj.transform.position;
                starPos.x = transform.position.x + Random.Range(0, minXDist);
                starPos.y = randomY;
                starObj.transform.position = starPos;
            }
        }
    }
}
