using System.Collections;
using UnityEngine;

public class CannonSpawner : MonoBehaviour
{
    public GameObject cannonPrefab;
    private int cannonNumber = GameManager.instance.cannonNumber;
    private float spacing = 7.0f;
    private Vector3 cannonStartPosition = new(480.0f, 2.0f, 5.0f);

    void Start()
    {
        cannonStartPosition = transform.position;
        StartCoroutine(SpawnCannons());
    }
    
    IEnumerator SpawnCannons()
    {
        for (int i = 0; i < cannonNumber; i++)
        {
            Vector3 brickPosition = cannonStartPosition + Vector3.right * spacing * i;
            GameObject cannon = Instantiate(cannonPrefab, brickPosition, Quaternion.identity, transform);
            cannon.transform.Rotate(-90.0f, 180.0f, 0.0f);
            yield return null;
        }
    }
}
