using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public GameObject explosionEffectPrefab;

    private bool explosionEffectActive = GameManager.instance.explosionEffectActive;
    private bool explode = false;


    void OnCollisionEnter(Collision collision)
    {
        if (!explosionEffectActive) return;

        if (!explode && collision.gameObject.CompareTag("Brick"))
        {
            explode = true;
            ContactPoint contactPoint = collision.contacts[0]; 
            GameObject explosionEffect = Instantiate(explosionEffectPrefab, contactPoint.point, Quaternion.identity);
            ParticleSystem[] particleSystems = explosionEffect.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Play();
            }
            Destroy(explosionEffect, 1.0f); // Destroy after 1 second
        }
    }
}

