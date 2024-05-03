using System.Collections;
using UnityEngine;

public class CannonSpawner : MonoBehaviour
{
    public GameObject cannonPrefab;
    public int count = 20;
    public float spacing = 7.0f;

    private Vector3 cannonStartPosition = new(480.0f, 2.0f, 5.0f);

    void Start()
    {
        StartCoroutine(SpawnCannons());
    }
    
    IEnumerator SpawnCannons()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 brickPosition = cannonStartPosition + Vector3.right * spacing * i;
            GameObject cannon = Instantiate(cannonPrefab, brickPosition, Quaternion.identity, transform);
            cannon.transform.Rotate(-90.0f, 180.0f, 0.0f);
            yield return null;
        }
    }
}
