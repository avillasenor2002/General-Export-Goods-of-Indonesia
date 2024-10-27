using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [System.Serializable]
    public class SoundEffect
    {
        public string name;         // Name identifier for the sound
        public AudioClip clip;      // AudioClip for the sound
    }

    public List<SoundEffect> soundEffects = new List<SoundEffect>(); // List of sound effects
    public AudioSource audioSource;   // Reference to the AudioSource component

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Play sound by name
    public void PlaySoundName(string name)
    {
        SoundEffect sfx = soundEffects.Find(s => s.name == name);
        if (sfx != null && sfx.clip != null)
        {
            audioSource.PlayOneShot(sfx.clip);
        }
        else
        {
            Debug.LogWarning($"Sound with name '{name}' not found or clip is missing.");
        }
    }

    // Play sound by index
    public void PlaySoundIndex(int index)
    {
        if (index >= 0 && index < soundEffects.Count && soundEffects[index].clip != null)
        {
            audioSource.PlayOneShot(soundEffects[index].clip);
        }
        else
        {
            Debug.LogWarning($"Sound at index '{index}' is out of range or clip is missing.");
        }
    }
}
