using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CastleBuilder : MonoBehaviour
{
    public GameObject brickPrefab; // Assign your brick prefab in the Unity Editor
    public float xCastleStart = 470.0f;
    public float zCastleStart = 90.0f;
    public int castleWidth = 42; // Number of bricks wide
    public int castleHeight = 7; // Number of bricks high
    public int castleDepth = 22; // Number of bricks depth
    public int castleThickness = 2; // Thickness of the walls
    public int bricksPerSecond = 100000;
    
    private List<Rigidbody> rigidBodies;
    private bool isCastleBuilt = false;

    void Start()
    {
        int brickNumber = 2*castleThickness*(castleWidth+castleDepth-2*castleThickness)*(castleHeight+1);
        rigidBodies = new List<Rigidbody>(brickNumber);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !isCastleBuilt)
        {
            StartCoroutine(BuildCastle(xCastleStart, zCastleStart));
        }
    }

    IEnumerator BuildCastle(float xStart, float zStart)
    {
        isCastleBuilt = true;
        Vector3 brickSize = brickPrefab.transform.localScale;
        Vector3 castleStartPosition = new(xStart, brickSize.y/2.0f, zStart);

        for (int y = 0; y < castleHeight; y++)
        {
            for (int z = 0; z < castleDepth; z++)
            {
                if (z < castleThickness || z >= castleDepth-castleThickness)
                {
                    for (int x = 0; x < castleWidth; x++)
                    {
                         Vector3 brickPosition = castleStartPosition + 
                        new Vector3(
                            x * brickSize.x, 
                            y * brickSize.y,
                            z * brickSize.z
                        );
                        GameObject brick = Instantiate(brickPrefab, brickPosition, Quaternion.identity, transform);
                        Rigidbody rb = brick.GetComponent<Rigidbody>();
                        rigidBodies.Add(rb);
                        yield return null;
                    }
                } else 
                {
                    for (int x = 0; x < castleWidth; x += (x==castleThickness-1) ? castleWidth-2*castleThickness+1 : 1)
                    {
                        Vector3 brickPosition = castleStartPosition + 
                            new Vector3(
                                x * brickSize.x, 
                                y * brickSize.y,
                                z * brickSize.z
                            );
                        GameObject brick = Instantiate(brickPrefab, brickPosition, Quaternion.identity, transform);
                        Rigidbody rb = brick.GetComponent<Rigidbody>();
                        rigidBodies.Add(rb);
                        yield return null;
                    }
                }
            }
        }

        for (int z = 0; z < castleDepth; z++)
        {
            if (z < castleThickness || z >= castleDepth-castleThickness)
            {
                for (int x = 0; x < castleWidth; x++)
                {
                    if (x%(2*castleThickness) == 0 || x%(2*castleThickness) == 1)
                    {
                        Vector3 brickPosition = castleStartPosition + 
                        new Vector3(
                            x * brickSize.x, 
                            castleHeight * brickSize.y,
                            z * brickSize.z
                        );
                        GameObject brick = Instantiate(brickPrefab, brickPosition, Quaternion.identity, transform);
                        Rigidbody rb = brick.GetComponent<Rigidbody>();
                        rigidBodies.Add(rb);
                        yield return null;
                    }
                }
            } else 
            {
                for (int x = 0; x < castleWidth; x += (x==castleThickness-1) ? castleWidth-2*castleThickness+1 : 1)
                {
                    if (z%(2*castleThickness) == 0 || z%(2*castleThickness) == 1)
                    {
                        Vector3 brickPosition = castleStartPosition + 
                        new Vector3(
                            x * brickSize.x, 
                            castleHeight * brickSize.y,
                            z * brickSize.z
                        );
                        GameObject brick = Instantiate(brickPrefab, brickPosition, Quaternion.identity, transform);
                        Rigidbody rb = brick.GetComponent<Rigidbody>();
                        rigidBodies.Add(rb);
                        yield return null;
                    }
                }
            }
        }
        
        foreach (Rigidbody rb in rigidBodies)
        {
            rb.freezeRotation = false;
        }
    }
}
