using System.Collections;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public float duration = 3.0f;                   // Total duration of the effect
    public float initialFlickerFrequency = 0.5f;    // Initial flicker frequency in seconds
    public float maxFlickerFrequency = 0.05f;       // Maximum flicker frequency
    public Color greyedOutColor = Color.grey;       // The greyed-out color for the sprite
    public Color highlightColor = Color.white;      // The color to flicker to
    public ParticleSystem defeatParticles;          // Reference to the particle system to activate

    private SpriteRenderer spriteRenderer;
    private float elapsedTime = 0.0f;
    private bool isHighlighted = false;

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Set sprite to greyed-out color at start
            spriteRenderer.color = highlightColor;
        }

        // Ensure the particle system is inactive at start
        //if (defeatParticles != null)
        //{
        //    defeatParticles.Stop();
        //}

        // Start the flicker coroutine
        //StartCoroutine(FlickerEffect());
    }

    public void BeginFlicker()
    {
        while (elapsedTime < duration)
        {
            Debug.Log("aaa");
            elapsedTime += Time.deltaTime;

            float currentFrequency;
            // Calculate the current flicker frequency to decrease the wait time between flickers over time
            currentFrequency = Mathf.Lerp(initialFlickerFrequency, maxFlickerFrequency, elapsedTime / duration);

            // Toggle the sprite color between greyed-out and highlighted
            isHighlighted = !isHighlighted;
            spriteRenderer.color = isHighlighted ? highlightColor : greyedOutColor;

            // Wait for the current frequency duration
        }

        // When the timer ends, activate the particle system
        //if (defeatParticles != null)
        //{
        //    defeatParticles.Play();
        //}
    }

    //public IEnumerator FlickerEffect()
    //{
        //while (elapsedTime < duration)
        //{
        //    Debug.Log("aaa");
        //    elapsedTime += Time.deltaTime;

        //    float currentFrequency;
        //    // Calculate the current flicker frequency to decrease the wait time between flickers over time
        //    currentFrequency = Mathf.Lerp(initialFlickerFrequency, maxFlickerFrequency, elapsedTime / duration);

        //    // Toggle the sprite color between greyed-out and highlighted
        //    isHighlighted = !isHighlighted;
        //    spriteRenderer.color = isHighlighted ? highlightColor : greyedOutColor;

        //    // Wait for the current frequency duration
        //    yield return new WaitForSeconds(currentFrequency);
        //}

        //// When the timer ends, activate the particle system
        //if (defeatParticles != null)
        //{
        //    defeatParticles.Play();
        //}
    //}
}
