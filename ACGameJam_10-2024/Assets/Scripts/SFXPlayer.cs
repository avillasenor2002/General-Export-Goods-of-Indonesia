using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public SoundEffectsDatabase soundEffectsDatabase; // Reference to the SoundEffectsDatabase
    public AudioSource audioSource;   // Reference to the AudioSource component

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (soundEffectsDatabase == null)
        {
            Debug.LogWarning("SoundEffectsDatabase is not assigned to SFXPlayer.");
        }
    }

    // Play sound by name
    public void PlaySoundName(string name)
    {
        if (soundEffectsDatabase == null) return;

        SoundEffect sfx = soundEffectsDatabase.soundEffects.Find(s => s.name == name);
        if (sfx != null && sfx.clip != null)
        {
            audioSource.PlayOneShot(sfx.clip);
        }
        else
        {
            Debug.LogWarning($"Sound with name '{name}' not found in database or clip is missing.");
        }
    }

    // Play sound by index
    public void PlaySoundIndex(int index)
    {
        if (soundEffectsDatabase == null) return;

        if (index >= 0 && index < soundEffectsDatabase.soundEffects.Count && soundEffectsDatabase.soundEffects[index].clip != null)
        {
            audioSource.PlayOneShot(soundEffectsDatabase.soundEffects[index].clip);
        }
        else
        {
            Debug.LogWarning($"Sound at index '{index}' is out of range in database or clip is missing.");
        }
    }
}
