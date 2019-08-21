using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField]
    private GameObject groundObj;
    [SerializeField]
    private GameObject[] treePrefabs;
    [SerializeField]
    private float minTreeRange = 1f;
    [SerializeField]
    private float maxTreeRange = 5f;
    [SerializeField]
    private float minY, maxY;
    [SerializeField]
    private float minX, maxX;

    private List<Transform> treeObjs = new List<Transform>();


    private void Start()
    {
        SpawnTree();
    }
    private void Update()
    {
        GroundFollowPlayer();
        MoveTrees();
    }

    private void GroundFollowPlayer()
    {
        Vector3 groundPos = groundObj.transform.position;
        groundPos.x = Player.Inst.transform.position.x;
        groundObj.transform.position = groundPos;
    }
    private void SpawnTree()
    {
        // Spawn First Tree
        Vector2 spawnPos = new Vector2(minX + Random.Range(0, maxTreeRange), Random.Range(minY, maxY));
        Transform tree = Instantiate(treePrefabs[Random.Range(0, treePrefabs.Length)], groundObj.transform).transform;
        tree.position = spawnPos;
        tree.localScale = new Vector3(Random.value == 0 ? 1 : -1, tree.localScale.y, tree.localScale.z);

        treeObjs.Add(tree);

        // Spawn Trees
        while (spawnPos.x <= maxX)
        {
            spawnPos = new Vector2(spawnPos.x + Random.Range(minTreeRange, maxTreeRange), Random.Range(minY, maxY));

            tree = Instantiate(treePrefabs[Random.Range(0, treePrefabs.Length)], groundObj.transform).transform;
            tree.position = spawnPos;
            tree.localScale = new Vector3(Random.value == 0 ? 1 : -1, tree.localScale.y, tree.localScale.z);

            treeObjs.Add(tree);
        }
    }
    private void MoveTrees()
    {
        for (int i = 0; i < treeObjs.Count; i++)
        {
            treeObjs[i].transform.Translate(Vector3.left * SkySunRising.Instance.MoveSpeed * Time.deltaTime, Space.World);
            if (treeObjs[i].localPosition.x < minX)
            {
                Vector3 pos = treeObjs[i].localPosition;
                pos.x = maxX;
                treeObjs[i].localPosition = pos;
            }
        }
    }
}
