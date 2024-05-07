using UnityEngine;

public class CameraCannonBallSpawner : MonoBehaviour
{
    public GameObject cannonBallPrefab;
    private float speed = 100.0f;
    private float fadeSeconds = 5.0f;

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
        Destroy(cannonBall, fadeSeconds);
    }
}
