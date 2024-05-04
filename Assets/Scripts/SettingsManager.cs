using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public bool enableParticles = true;
    public bool enableAttackEffects = true;
    // public bool enableMagicEffects = true;
    public bool enableSound = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleParticles()
    {
        enableParticles = !enableParticles;
    }

    public void ToggleAttackEffects()
    {
        enableAttackEffects = !enableAttackEffects;
    }
    // public void ToggleBloodEffects()
    // {
    //     enableBloodEffects = !enableBloodEffects;
    // }
    //
    // public void ToggleMagicEffects()
    // {
    //     enableMagicEffects = !enableMagicEffects;
    // }
    
    public void ToggleSound()
    {
        enableSound = !enableSound;
    }
}
