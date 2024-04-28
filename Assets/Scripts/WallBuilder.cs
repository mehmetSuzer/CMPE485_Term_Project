using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WallBuilder : MonoBehaviour
{
    public GameObject brickPrefab; // Assign your brick prefab in the Unity Editor
    public int wallWidth = 50; // Number of bricks wide
    public int wallHeight = 20; // Number of bricks high
    public int wallDepth = 2; // Number of bricks depth
    public float spacing = 0.00f; // Spacing etween adjacent bricks

    private const int bricksPerSecond = 100000;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(1.0f / bricksPerSecond);
    
    private const int maxBrickCount = 40000;
    private List<GameObject> spawnedBricks = new List<GameObject>(maxBrickCount); // List to store instantiated bricks

    private bool wallBuilt = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !wallBuilt)
        {
            StartCoroutine(BuildWall());
        }
    }

    IEnumerator BuildWall()
    {
        wallBuilt = true;
        Vector3 brickSize = brickPrefab.transform.localScale;
        Vector3 startPosition = transform.position;

        for (int y = 0; y < wallHeight; y++)
        {
            for (int x = 0; x < wallWidth; x++)
            {
                for (int z = 0; z < wallDepth; z++)
                {
                    Vector3 brickPosition = startPosition + 
                        new Vector3(
                            x * (brickSize.x + spacing), 
                            (y-0.5f) * brickSize.y,
                            z * (brickSize.z + spacing)
                        );
                    GameObject brick = Instantiate(brickPrefab, brickPosition, Quaternion.identity, transform);
                    // spawnedBricks.Add(brick);
                    yield return null;
                }
            }
        }
    }

    void clearWall()
    {
        spawnedBricks.Clear();
        wallBuilt = false;
    }
}
