using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAudioController : MonoBehaviour
{
    #region Cached
    [SerializeField] AudioClip[] rocketSounds;
    private AudioSource audioSource;
    #endregion

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleSFX(int i, bool loop, bool toggle) {
        if (toggle)
            PlaySFX(i, loop);
        else {
            StopSFX(i);
        }
    }

    public bool PlaySFX(int i, bool loop) {
        try {
            if (audioSource.clip != rocketSounds[i]) {
                audioSource.clip = rocketSounds[i];
                audioSource.Play();
                audioSource.loop = loop;
            }
        }
        catch (IndexOutOfRangeException e) {
            Debug.Log("Error: Invalid index when trying to access VFX array.\n\n" + e);
            return false;
        }
        return true;
    }

    public bool StopSFX(int i) {
        try {
            if (audioSource.clip == rocketSounds[i]) {
                audioSource.Stop();
            }
        }
        catch (IndexOutOfRangeException e) {
            Debug.Log("Error: Invalid index when trying to access VFX array.\n\n" + e);
            return false;
        }
        return true;
    }
}
