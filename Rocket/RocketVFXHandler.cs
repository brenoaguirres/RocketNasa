using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketVFXHandler : MonoBehaviour
{
    #region Cached 
    [SerializeField] ParticleSystem[] rocketEffects;
    #endregion

    public void ToggleVFX(int i, bool toggle) {
        if (toggle)
            PlayVFX(i);
        else {
            StopVFX(i);
        }
    }

    public bool PlayVFX(int i) {
        try {
            if (!rocketEffects[i].isPlaying) {
                rocketEffects[i].gameObject.SetActive(true);
                rocketEffects[i].Play();
            }
        }
        catch (IndexOutOfRangeException e) {
            Debug.Log("Error: Invalid index when trying to access VFX array.\n\n" + e);
            return false;
        }
        return true;
    }

    public bool StopVFX(int i) {
        try {
            if (!rocketEffects[i].isStopped) {
                rocketEffects[i].Stop();
                rocketEffects[i].gameObject.SetActive(false);
            }
        }
        catch (IndexOutOfRangeException e) {
            Debug.Log("Error: Invalid index when trying to access VFX array.\n\n" + e);
            return false;
        }
        return true;
    }

    public void StopAllVFX() {
        foreach (ParticleSystem particle in rocketEffects) {
            particle.Stop();
            particle.gameObject.SetActive(false);
        }
    }
}
