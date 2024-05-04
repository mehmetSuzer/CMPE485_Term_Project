using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class CannonController : MonoBehaviour
{
    public GameObject cannonBallPrefab;
    public GameObject cannonBlastPrefab;
    public float cannonBallSpeed = 100.0f;
    public float attackAngle = -10.0f;
    public float cannonBallFadeSeconds = 5.0f;

    public float attackSecondLow = 3.0f;
    public float attackSecondHigh = 8.0f;

    private bool attacking = false;
    private Vector3 cannonBallVelocity;
    private Animator animator;
    [SerializeField] private AudioClip fireSfx;

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

    void Blast()
    {
        Vector3 position = transform.position + new Vector3(0.0f, 1.5f, 4.0f);
        if (SettingsManager.Instance.enableParticles)
        {
            GameObject explosionEffect = Instantiate(cannonBlastPrefab, position, Quaternion.identity);
            ParticleSystem[] particleSystems = explosionEffect.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Play();
            }

            Destroy(explosionEffect, 1.0f); // Destroy after 1 second
        }

        if (SettingsManager.Instance.enableSound)
        {
            //TODO: Play sound
            print("play sound");
            AudioSource.PlayClipAtPoint(fireSfx, transform.position);
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(attackSecondLow, attackSecondHigh));
            if (attacking)
            {
                GameObject cannonBall =
                    Instantiate(cannonBallPrefab, transform.position, Quaternion.identity, transform);
                cannonBall.GetComponent<Rigidbody>().velocity = cannonBallVelocity;
                Destroy(cannonBall, cannonBallFadeSeconds);
                Blast();
            }
            else
            {
                break;
            }
        }
    }
}