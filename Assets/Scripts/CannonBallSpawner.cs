using UnityEngine;

public class CannonBallSpawner : MonoBehaviour
{
    public GameObject cannonBallPrefab;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        Rigidbody rb = cannonBall.GetComponent<Rigidbody>();
        rb.velocity = speed * transform.forward;;
    }
}
