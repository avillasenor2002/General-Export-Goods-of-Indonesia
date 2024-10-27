using System.Collections;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public float duration = 3.0f;                   // Total duration of the effect
    public float initialFlipFrequency = 0.5f;       // Initial flip frequency in seconds
    public float maxFlipFrequency = 0.05f;          // Maximum flip frequency
    public Color greyedOutColor = Color.grey;       // The greyed-out color for the sprite
    public Color highlightColor = Color.white;      // The color to flip to
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
            spriteRenderer.color = greyedOutColor;
        }

        // Ensure the particle system is inactive at start
        if (defeatParticles != null)
        {
            defeatParticles.Stop();
        }

        // Start the color flip coroutine
        //StartCoroutine(ColorFlipEffect());
    }

    public IEnumerator ColorFlipEffect()
    {
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the current flip frequency to increase speed over time
            float currentFrequency = Mathf.Lerp(initialFlipFrequency, maxFlipFrequency, elapsedTime / duration);

            // Toggle the sprite color between greyed-out and highlighted
            isHighlighted = !isHighlighted;
            spriteRenderer.color = isHighlighted ? highlightColor : greyedOutColor;

            // Wait for the current frequency duration before flipping again
            yield return new WaitForSeconds(currentFrequency);
            // When the timer ends, activate the particle system
            if (defeatParticles != null)
            {
                defeatParticles.Play();
            }
        }
    }
}
