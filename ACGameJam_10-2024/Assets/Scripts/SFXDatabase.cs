using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundEffect
{
    public string name;         // Name identifier for the sound
    public AudioClip clip;      // AudioClip for the sound
}

[CreateAssetMenu(fileName = "SoundEffectsDatabase", menuName = "Audio/SoundEffectsDatabase")]
public class SoundEffectsDatabase : ScriptableObject
{
    public List<SoundEffect> soundEffects = new List<SoundEffect>(); // List of sound effects
}