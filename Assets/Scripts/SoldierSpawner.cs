using System.Collections;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
    public GameObject[] soldierPrefabs;
    public LayerMask enemyLayer;

    // public Transform spawnLine; // Reference to the line spawner GameObject
    private const float spacing = 5f;
    private int soldierPerLine = GameManager.instance.soldierPerLine;
    private int lineNumber = GameManager.instance.lineNumber;
    private bool spawningStarted = false;

    void Update()
    {
        if (!spawningStarted && Input.GetKeyDown(KeyCode.B)) // Start spawning soldiers
        {
            spawningStarted = true;
            StartCoroutine(SpawnSoldiersCoroutine());
        }
    }

    IEnumerator SpawnSoldiersCoroutine()
    {
        for (int i = 0; i < lineNumber; i++)
        {
            for (int j = 0; j < soldierPerLine; j++)
            {
                GameObject soldierPrefab = soldierPrefabs[Random.Range(0, soldierPrefabs.Length)];

                Vector3 spawnPosition = transform.position + Vector3.right * j * spacing + -transform.forward * (i * spacing);

                var soldier = Instantiate(soldierPrefab, spawnPosition, transform.rotation, transform);
                soldier.layer = gameObject.layer;
                soldier.GetComponent<SoldierController>().SetParameters(enemyLayer, spacing);
                // soldier.GetComponent<SoldierController>().set

                yield return null;
            }
        }
    }
}
