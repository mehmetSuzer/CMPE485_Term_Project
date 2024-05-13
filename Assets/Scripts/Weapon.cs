using DefaultNamespace;
using ObjectPools;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public LayerMask enemyLayer;
    public GameObject attackEffectPrefab;
    
    [SerializeField] 
    [Tooltip("Only valid if object pooling is active")]
    private AttackEffectType attackEffectType;
    
    private bool bloodMagicEffectActive = GameManager.instance.bloodMagicEffectActive;
    private float attackEffectFadeSeconds = GameManager.instance.attackEffectLifetime;

    private void OnTriggerEnter(Collider other)
    {
        if (!other) return;

        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            if (bloodMagicEffectActive)
            {
                Vector3 position = transform.position + transform.forward * 2.0f;
                if (GameManager.instance.ObjectPoolingActive)
                {
                    GameObject attackEffect =
                        AttackEffectPool.Instance.GetPooledObject(attackEffectType, GameManager.instance.attackEffectLifetime);
                    attackEffect.transform.SetPositionAndRotation(position, Quaternion.identity);
                }
                else
                {
                    GameObject attackEffect = Instantiate(attackEffectPrefab, position, Quaternion.identity);
                    Destroy(attackEffect, attackEffectFadeSeconds);
                }
            }

            other.gameObject.GetComponent<SoldierController>().Damage(10.0f);
        }
    }
}