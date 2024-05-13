using System;
using ObjectPools;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public GameObject explosionEffectPrefab;

    private bool explosionEffectActive;
    private bool explode = false;

    private void Start()
    {
        explosionEffectActive = GameManager.instance.explosionEffectActive;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!explosionEffectActive) return;

        if (!explode && collision.gameObject.CompareTag("Brick"))
        {
            explode = true;
            ContactPoint contactPoint = collision.contacts[0];

            if (GameManager.instance.ObjectPoolingActive)
            {
                GameObject explosionEffect =
                    ExplosionPool.Instance.GetPooledObject(1.0f);
                explosionEffect.transform.SetPositionAndRotation(contactPoint.point, Quaternion.identity);
                ParticleSystem[] particleSystems = explosionEffect.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem ps in particleSystems)
                {
                    ps.Play();
                }
            }
            else
            {
                GameObject explosionEffect =
                    Instantiate(explosionEffectPrefab, contactPoint.point, Quaternion.identity);
                ParticleSystem[] particleSystems = explosionEffect.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem ps in particleSystems)
                {
                    ps.Play();
                }

                Destroy(explosionEffect, 1.0f); // Destroy after 1 second
            }
        }
    }
}