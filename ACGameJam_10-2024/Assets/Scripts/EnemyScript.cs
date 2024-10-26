using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyForm
{
    public string formName;
    public int health;
    public Sprite formSprite; // Optional: Change the appearance based on form
}

public class EnemyScript : MonoBehaviour
{
    public EnemyData enemyData;           // Reference to the EnemyData ScriptableObject
    public int initialFormIndex = 0;      // Set the initial form in the Inspector
    public int currentFormIndex;
    public int currentHealth;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Load the initial form from the inspector setting
        if (enemyData != null && enemyData.forms.Count > 0)
        {
            currentFormIndex = Mathf.Clamp(initialFormIndex, 0, enemyData.forms.Count - 1);
            SetForm(currentFormIndex);
        }
        else
        {
            Debug.LogError("No forms found in enemy data!");
        }
    }

    void SetForm(int formIndex)
    {
        if (enemyData == null || formIndex < 0 || formIndex >= enemyData.forms.Count) return;

        currentFormIndex = formIndex;
        EnemyForm form = enemyData.forms[formIndex];
        currentHealth = form.health;

        if (form.formSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = form.formSprite;
        }

        Debug.Log($"Switched to form: {form.formName} with Health: {currentHealth}");
    }

    public void OnCollisionEnter2D(Collider2D collision)
    {
        // Check the tag of the colliding object
        if (collision.CompareTag("Wall"))
        {
            Destroy(this.gameObject, 0.5f); // Destroys the object after 2 seconds
        }
    }

    public void ChangeForm()
    {
        currentFormIndex = (currentFormIndex + 1) % enemyData.forms.Count;
        SetForm(currentFormIndex);
    }

    void Update()
    {
        // Placeholder for movement logic or other updates
    }
}