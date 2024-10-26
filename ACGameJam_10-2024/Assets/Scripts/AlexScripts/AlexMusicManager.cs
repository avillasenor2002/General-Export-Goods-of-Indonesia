using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlexMusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    public float volume;
    public AudioClip victoryClip;
    private Dictionary<string, AudioClip> sceneTracks = new Dictionary<string, AudioClip>();

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
        audioSource.loop = true;

        // Initialize the dictionary with scene names and their corresponding audio clips
        sceneTracks.Add("Scene1", Resources.Load<AudioClip>("Audio/Scene1Track"));
        sceneTracks.Add("Scene2", Resources.Load<AudioClip>("Audio/Scene2Track"));
        sceneTracks.Add("Scene3", Resources.Load<AudioClip>("Audio/Scene3Track"));
        sceneTracks.Add("Scene4", Resources.Load<AudioClip>("Audio/Scene4Track"));
        sceneTracks.Add("Scene5", Resources.Load<AudioClip>("Audio/Scene5Track"));
        sceneTracks.Add("Scene6", Resources.Load<AudioClip>("Audio/Scene6Track"));
        sceneTracks.Add("Scene7", Resources.Load<AudioClip>("Audio/Scene7Track"));
        sceneTracks.Add("Scene8", Resources.Load<AudioClip>("Audio/Scene8Track"));
        sceneTracks.Add("Scene9", Resources.Load<AudioClip>("Audio/Scene9Track"));
        sceneTracks.Add("Scene10", Resources.Load<AudioClip>("Audio/Scene10Track"));
    }

    void Update()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayTrackForScene(currentSceneName);
    }

    private void PlayTrackForScene(string sceneName)
    {
        if (sceneTracks.TryGetValue(sceneName, out AudioClip clip) && audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
