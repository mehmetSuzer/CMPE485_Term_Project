using System.Collections;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
    public GameObject[] soldierPrefabs;

    // public Transform spawnLine; // Reference to the line spawner GameObject
    public float spacing = 1.5f;
    public int maxSoldiersPerLine = 10;
    public int lines = 10;

    void Start()
    {
        StartCoroutine(SpawnSoldiersCoroutine());
    }

    IEnumerator SpawnSoldiersCoroutine()
    {

        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < maxSoldiersPerLine; j++)
            {
                GameObject soldierPrefab = soldierPrefabs[Random.Range(0, soldierPrefabs.Length)];

                Vector3 spawnPosition = transform.position + Vector3.forward * i * spacing + Vector3.right * j * spacing;

                Instantiate(soldierPrefab, spawnPosition, Quaternion.identity);


                yield return null;
            }
        }
    }
}