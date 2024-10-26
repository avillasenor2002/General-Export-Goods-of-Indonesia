using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexSFKeys : MonoBehaviour
{
    public AudioSource audioSource;
    //Place the clip
    public AudioClip sound;
    public float volume;

    //Ref this function into any script that plays sound
    public void KeySound()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound, volume);
    }
}
