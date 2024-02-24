using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    #region Cached
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider ambienceSlider;
    #endregion

    public void Start() {
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
        SetAmbienceVolume();
    }

    /// <summary>
    /// Sets the master channel of the audio mixer's volume to the value of the predefined respective slider.
    /// </summary>
    public void SetMasterVolume() {
        float volume = masterSlider.value;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume)*20);
    }
    
    /// <summary>
    /// Sets the music channel of the audio mixer's volume to the value of the predefined respective slider.
    /// </summary>
    public void SetMusicVolume() {
        float volume = musicSlider.value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume)*20);
    }

    /// <summary>
    /// Sets the SFX channel of the audio mixer's volume to the value of the predefined respective slider.
    /// </summary>
    public void SetSFXVolume() {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume)*20);
    }

    /// <summary>
    /// Sets the Ambience channel of the audio mixer's volume to the value of the predefined respective slider.
    /// </summary>
    public void SetAmbienceVolume() {
        float volume = ambienceSlider.value;
        audioMixer.SetFloat("AmbienceVolume", Mathf.Log10(volume)*20);
    }
}
