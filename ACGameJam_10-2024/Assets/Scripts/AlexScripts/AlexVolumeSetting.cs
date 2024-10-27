using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
public class AlexVolumeSetting : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider SFXSlider;
    const string mixer_music = "MusicVolume";
    const string mixer_SFX = "SFXVolume";
    
    void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void SetMusicVolume(float value)
    {
        audioMixer.SetFloat(mixer_music, Mathf.Log10(value) * 20);
    }
    void SetSFXVolume(float value)
    {
        audioMixer.SetFloat(mixer_SFX, Mathf.Log10(value) * 20);
    }
}
