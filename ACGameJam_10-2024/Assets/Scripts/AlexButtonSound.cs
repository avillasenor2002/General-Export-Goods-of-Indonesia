using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlexButtonSound : MonoBehaviour
{
    public AudioClip sound;
    public float volume;
    AudioSource audioSource;

    void Awake ()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        if (sound != null)
            audioSource.clip = sound;
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
        GetComponent<Button>().onClick.AddListener(()=>audioSource.Play ());
    }
}