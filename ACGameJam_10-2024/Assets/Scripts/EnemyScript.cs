using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyForm
{
    public string formName;
    public int health;
    public float size;
    public AnimationClip idle;
    public AnimationClip death;

    public Sprite formSprite; // Optional: Change the appearance based on form
}

public class EnemyScript : MonoBehaviour
{
    public EnemyData enemyData;           // Reference to the EnemyData ScriptableObject
    public int initialFormIndex = 0;      // Set the initial form in the Inspector
    public int currentFormIndex;
    public int currentHealth;
    public SpriteRenderer spriteRenderer;
    public BallMovement Balls;
    public AlexScreenShake ScreenShake;
    public bool isLaunched;
    public bool isDying;
    public float enemySize;
    public string spriteName;

    public ParticleSystem defeatParticles;

    public EnemyDeath DeathPart;
    public int timeToDie;

    public GameObject skeletonBonePile;
    public Animator myAnimator;

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
        enemySize = 1 + (form.size / 3);
        spriteName = form.formName;


        if (enemySize < 1)
        {
            enemySize = 1;
        }


        transform.localScale = new Vector2(enemySize, enemySize);
        currentHealth = form.health;

        if (form.formSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = form.formSprite;
        }

        // Play the idle animation for the current form if available
        if (form.idle != null && myAnimator != null)
        {
            myAnimator.Play(form.idle.name);
        }

        Debug.Log($"Switched to form: {form.formName} with Health: {currentHealth}");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyScript enScript = collision.gameObject.GetComponentInParent<EnemyScript>();
        if (isLaunched == true)
        {
            StartCoroutine(Flash());
        }
        if (enScript != null)
        {
            Debug.Log("is hit");
            if (enScript.currentHealth <= currentHealth)
            {
                Debug.Log("my health is greater");
                if (ScreenShake != null)
                {
                    ScreenShake.IsShaking();
                    DeathPart.duration = timeToDie;
                    DeathPart.StartCoroutine(DeathPart.ColorFlipEffect());
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
        if (ScreenShake != null)
        {
            ScreenShake.IsShaking();
        }
        Debug.Log("killing other enemy");

        // Play the death animation if available
        if (myAnimator != null && enemyData.forms[currentFormIndex].death != null)
        {
            myAnimator.Play(enemyData.forms[currentFormIndex].death.name);
        }

        yield return new WaitForSeconds(2);

        Instantiate(defeatParticles, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);

        Debug.Log("Current form is " + currentFormIndex);
        if (currentFormIndex == 8)
        {
            Instantiate(skeletonBonePile, enemy.transform.position, Quaternion.identity);
        }

        Destroy(enemy);
    }

    IEnumerator Flash()
    {
        while (true)
        {
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.gray;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ChangeForm()
    {
        currentFormIndex = (currentFormIndex + 1) % enemyData.forms.Count;
        SetForm(currentFormIndex);
    }

    void Update()
    {
        // Additional logic can be added here
    }
}
