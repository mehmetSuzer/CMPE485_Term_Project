using UnityEngine;

public class CannonBallSpawner : MonoBehaviour
{
    public GameObject cannonBallPrefab;
    public float speed = 100.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnCannonBall();
        }
    }

    void SpawnCannonBall()
    {
        GameObject cannonBall = Instantiate(cannonBallPrefab,  transform.position, Quaternion.identity);
        cannonBall.GetComponent<Rigidbody>().velocity = speed * transform.forward;
    }
}
