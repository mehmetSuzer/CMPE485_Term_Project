using System.Collections;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject cannonBallPrefab;
    public GameObject cannonBlastPrefab;
    public AudioClip fireSfx;

    private bool explosionEffectActive = GameManager.instance.explosionEffectActive;
    private float cannonBallSpeed = 100.0f;
    private float attackAngle = -13.5f;
    private float attackSecondLow = 3.0f;
    private float attackSecondHigh = 8.0f;
    private float cannonBallFadeSeconds = GameManager.instance.cannonBallLifetime;
    private float explosionEffectFadeSeconds = 1.0f;
    private bool attacking = false;
    private Vector3 cannonBallVelocity;

    void Start()
    {
        Quaternion rotation = Quaternion.Euler(attackAngle, 0, 0);
        cannonBallVelocity = rotation * Vector3.forward * cannonBallSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) // Start and stop cannon fire
        {
            attacking = !attacking;
            StartCoroutine(Attack());
        }
    }

    void Blast()
    {
        Vector3 position = transform.position + new Vector3(0.0f, 1.5f, 4.0f);
        GameObject explosionEffect = Instantiate(cannonBlastPrefab, position, Quaternion.identity);
        ParticleSystem[] particleSystems = explosionEffect.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play();
        }
        AudioSource.PlayClipAtPoint(fireSfx, transform.position);

        Destroy(explosionEffect, explosionEffectFadeSeconds);
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(attackSecondLow, attackSecondHigh));
            if (attacking)
            {
                GameObject cannonBall = Instantiate(cannonBallPrefab, transform.position, Quaternion.identity, transform);
                cannonBall.GetComponent<Rigidbody>().velocity = cannonBallVelocity;
                Destroy(cannonBall, cannonBallFadeSeconds);
                if (explosionEffectActive)
                {
                    Blast();
                }
            }
            else
            {
                break;
            }
        }
    }
}