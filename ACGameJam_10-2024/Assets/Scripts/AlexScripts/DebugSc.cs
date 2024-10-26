using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSc : MonoBehaviour
{
    public string scene1;
    private AudioSource audioSource;
    public float volume;
    public AudioClip sceneTrack1;

    void Awake()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
        audioSource.loop = true;
    }

    void Update()
    {
        Manager();
    }

    public void Manager()
    {
        Debug.Log("Current Scene: " + SceneManager.GetActiveScene().name);
        
        if (SceneManager.GetActiveScene().name == scene1 && audioSource.clip != sceneTrack1)
        {
            audioSource.clip = sceneTrack1;
            audioSource.volume = volume;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Playing Scene 1 Track");
        }
    }
}