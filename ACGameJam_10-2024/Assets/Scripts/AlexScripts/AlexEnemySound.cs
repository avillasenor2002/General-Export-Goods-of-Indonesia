using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexEnemySound : MonoBehaviour
{
    public AnimationClip animationIdleClip;
    public AnimationClip animationWalkClip;
    public AnimationClip animationAttackClip;
    public AnimationClip animationHurtClip;
    public AnimationClip animationDeathClip;
    public AudioClip idleSound;
    public AudioClip walkSound;
    public AudioClip attackSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public float idleVolume;
    public float walkVolume;
    public float attackVolume;
    public float hurtVolume;
    public float deathVolume;

    private AudioSource audioSource;
    private Animator animator;
    private AnimatorStateInfo stateInfo;

    private void Awake() 
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        animator = this.gameObject.GetComponent<Animator>();
    }

    public void Idle()
    {
        if(stateInfo.IsName(animationIdleClip.name))
        {
            audioSource.clip = idleSound;
            audioSource.volume = idleVolume;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
    public void Walk()
    {
        if(stateInfo.IsName(animationIdleClip.name))
        {
            audioSource.clip = walkSound;
            audioSource.volume = walkVolume;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
    public void Attack()
    {
        if(stateInfo.IsName(animationIdleClip.name))
        {
            audioSource.clip = attackSound;
            audioSource.volume = attackVolume;
            audioSource.PlayOneShot(attackSound);
        }
    }
    public void Hurt()
    {
        if(stateInfo.IsName(animationIdleClip.name))
        {
            audioSource.clip = hurtSound;
            audioSource.volume = hurtVolume;
            audioSource.PlayOneShot(hurtSound);
        }
    }
    public void Death()
    {
        if(stateInfo.IsName(animationIdleClip.name))
        {
            audioSource.clip = deathSound;
            audioSource.volume = deathVolume;
            audioSource.PlayOneShot(deathSound);
        }
    }
    private void Update() 
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Idle();
        Walk();
        Attack();
        Hurt();
        Death();
    }
}
