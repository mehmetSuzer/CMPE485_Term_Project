using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CastleBuilder : MonoBehaviour
{
    public GameObject smallBrickPrefab;
    public GameObject mediumBrickPrefab;
    public GameObject largeBrickPrefab;

    private float xCastleStart = 470.0f;
    private float zCastleStart = 90.0f;
    private GameObject brickPrefab; // Assign your brick prefab
    private int castleWidth = GameManager.instance.castleWidth; // Number of bricks wide
    private int castleHeight = GameManager.instance.castleHeight; // Number of bricks high
    private int castleDepth = GameManager.instance.castleDepth; // Number of bricks depth
    private const int castleThickness = 2; // Thickness of the walls
    private List<Rigidbody> rigidBodies;

    void Start()
    {
        xCastleStart = transform.position.x + 20.0f;
        zCastleStart = transform.position.z + 10.0f;

        brickPrefab = (GameManager.instance.brickType == BrickType.Small) ? smallBrickPrefab
                    : (GameManager.instance.brickType == BrickType.Medium) ? mediumBrickPrefab
                    : largeBrickPrefab;
        
        int brickNumber = 2*castleThickness*(castleWidth+castleDepth-2*castleThickness)*(castleHeight+1);
        rigidBodies = new List<Rigidbody>(brickNumber);
        StartCoroutine(BuildCastle());
    }

    IEnumerator BuildCastle()
    {
        Vector3 brickSize = brickPrefab.transform.localScale;
        Vector3 castleStartPosition = new(xCastleStart, brickSize.y/2.0f, zCastleStart);

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
