using UnityEngine;

public class Weapon : MonoBehaviour
{
    public LayerMask enemyLayer;
    public GameObject attackEffectPrefab;
    private bool bloodMagicEffectActive = GameManager.instance.bloodMagicEffectActive;
    private float attackEffectFadeSeconds = GameManager.instance.attackEffectLifetime;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other) return;
        
        if  (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            if (bloodMagicEffectActive)
            {  
                GameObject attackEffect = Instantiate(attackEffectPrefab, transform.position + transform.forward * 2.0f, Quaternion.identity);
                Destroy(attackEffect, attackEffectFadeSeconds);
            }
            other.gameObject.GetComponent<SoldierController>().Damage(20.0f);
        }
    }
}
