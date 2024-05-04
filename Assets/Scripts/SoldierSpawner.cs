using System.Collections;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] soldierPrefabs;

    // public Transform spawnLine; // Reference to the line spawner GameObject
    [SerializeField] private float spacing = 1.5f;
    [SerializeField] private int maxSoldiersPerLine = 10;
    public int lines = 10;
    [SerializeField] private LayerMask enemyLayer;
    
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

                Vector3 spawnPosition = transform.position + Vector3.right * j * spacing + Vector3.forward * i * spacing;

                var soldier = Instantiate(soldierPrefab, spawnPosition, transform.rotation, transform);
                soldier.layer = gameObject.layer;
                soldier.GetComponent<SoldierController>().SetParameters(enemyLayer, spacing);
                // soldier.GetComponent<SoldierController>().set

                yield return null;
            }
        }
    }
}