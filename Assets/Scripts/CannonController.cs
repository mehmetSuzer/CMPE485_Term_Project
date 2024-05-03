using System.Collections;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject cannonBallPrefab;
    public float cannonBallSpeed = 100.0f;
    public float attackAngle = -10.0f; 

    private bool attacking = false;
    private Vector3 cannonBallVelocity;
    private Animator animator;
    void Start()
    {
        Quaternion rotation = Quaternion.Euler(attackAngle, 0, 0);
        cannonBallVelocity = rotation * Vector3.forward * cannonBallSpeed;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) // Start and stop cannon fire
        {
            attacking = !attacking;
            StartCoroutine(Attack());
        }
    }
    
    IEnumerator Attack()
    {
        while (attacking)
        {
            yield return new WaitForSeconds(Random.Range(3.0f, 8.0f));
            GameObject cannonBall = Instantiate(cannonBallPrefab, transform.position, Quaternion.identity, transform);
            cannonBall.GetComponent<Rigidbody>().velocity = cannonBallVelocity;
        }
    }
}
