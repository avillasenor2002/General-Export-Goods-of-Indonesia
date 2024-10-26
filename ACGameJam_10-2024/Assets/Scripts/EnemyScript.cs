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
    public BallMovement Balls;
    public AlexScreenShake ScreenShake;
    public bool isLaunched;
    public bool isDying;

    void Start()
    {
        isDying = false;
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


        BallMovement Balls = FindObjectOfType<BallMovement>();
        AlexScreenShake ScreenShake = Camera.main.GetComponent<AlexScreenShake>();

        if (Balls != null)
        {
            Balls = Balls.GetComponent<BallMovement>();
            Debug.Log("Found object with TargetScript: " + Balls.gameObject.name);
            // You can now access targetObject's properties and methods
        }
        else
        {
            Debug.Log("No object with TargetScript found in the scene.");
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyScript enScript = collision.gameObject.GetComponentInParent<EnemyScript>();
        if (enScript != null)
        {
            Debug.Log("is hit");
            if (enScript.currentHealth <= currentHealth)
            {
                Debug.Log("my health is greater");
                if (ScreenShake != null)
                {
                    ScreenShake.IsShaking();
                }
                if (isLaunched == true)
                {

                    StartCoroutine(BelatedDeath(collision.gameObject));
                    enScript.StartCoroutine(BelatedDeath(enScript.gameObject));
                    enScript.isLaunched = true;

                }
                //GrowPlayer(enScript.currentHealth);
            }
        }
    }

    IEnumerator BelatedDeath(GameObject enemy)
    {
        Debug.Log("killing other enemy");
        yield return new WaitForSeconds(2);
        Destroy(enemy);
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