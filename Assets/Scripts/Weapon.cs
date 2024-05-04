using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public LayerMask enemyLayer;
    public GameObject attackEffect;

    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other)
            return;
        
        if  (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            var settingsManager = SettingsManager.Instance;
            if(settingsManager.enableParticles && settingsManager.enableAttackEffects)
                Instantiate(attackEffect, transform.position + transform.forward * 2, Quaternion.identity);
            other.gameObject.GetComponent<SoldierController>().Damage(20f);
        }
    }
}
