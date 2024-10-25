using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [System.Serializable]
    public class EnemyForm
    {
        public string formName;
        public int health;
        public Sprite formSprite; // Optional: Change the appearance based on form
    }

    public List<EnemyForm> forms = new List<EnemyForm>();
    private int currentFormIndex = 0;
    private int currentHealth;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (forms.Count > 0)
        {
            SetForm(currentFormIndex);
        }
    }

    void SetForm(int formIndex)
    {
        if (formIndex < 0 || formIndex >= forms.Count) return;

        currentFormIndex = formIndex;
        EnemyForm form = forms[formIndex];
        currentHealth = form.health;

        if (form.formSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = form.formSprite;
        }

        Debug.Log($"Switched to form: {form.formName} with Health: {currentHealth}");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check the tag of the colliding object
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject, 2f); // Destroys the object after 2 seconds
        }
        else if (collision.CompareTag("Projectile"))
        {
            Destroy(gameObject, 2f); // Destroys the object after 2 seconds
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void ChangeForm()
    {
        currentFormIndex = (currentFormIndex + 1) % forms.Count;
        SetForm(currentFormIndex);
    }

    void Update()
    {
        // Movement logic based on `currentSpeed`
    }
}
