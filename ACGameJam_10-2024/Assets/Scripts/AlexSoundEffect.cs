using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexSoundEffect : MonoBehaviour
{
    public AudioSource audioSource;
    //Place the clip
    public AudioClip sound;
    public float volume;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        //Feel free to change the tag to anything
        if(collision.gameObject.tag == "Anything")
        {
            audioSource.PlayOneShot(sound, volume);
        }
    }

}
