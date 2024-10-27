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
    public Rigidbody2D enemyRB;
    public EnemyDeath enemyDeath;
    public bool isLaunched;
    public bool isDying;
    public bool isMyTurn;
    public GameObject player;
    public Color solidGhost;
    public Color transGhost;
    public ParticleSystem defeatParticles;

    void Start()
    {
        solidGhost = new Color(1, 1, 1, 1);
        transGhost = new Color(1, 1, 1, 0.5f);
        isDying = false;
        enemyRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyDeath = GetComponent<EnemyDeath>();

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

        player = GameObject.Find("PlayerSlime");
        if (player != null)
        {
            Balls = player.GetComponent<BallMovement>();
        }

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

                if (isLaunched == true)
                {
                    enScript.StartCoroutine(BelatedDeath(enScript.gameObject));
                    //enemyDeath.BeginFlicker();
                    enScript.isLaunched = true;
                }
            }
        }

        if (Balls != null)
        {
            if (Balls.playerHealth <= currentHealth)
            {
                if (isLaunched == true)
                {
                    StartCoroutine(BelatedDeath(this.gameObject));
                }
            }
        }
    }

    IEnumerator BelatedDeath(GameObject enemy)
    {
        //Instantiate(defeatParticles, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
        if (ScreenShake != null)
        {
            ScreenShake.IsShaking();
        }
        Debug.Log("killing other enemy");
        yield return new WaitForSeconds(2);
        Instantiate(defeatParticles, new Vector3 (enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), Quaternion.identity);
        Destroy(enemy);
    }

    public void ChangeForm()
    {
        currentFormIndex = (currentFormIndex + 1) % enemyData.forms.Count;
        SetForm(currentFormIndex);
    }

    void Update()
    {
        if (Balls.isMyTurn == false)
        {
            if (Balls.isMoving == false)
            {
                if (enemyRB.simulated == true)
                {
                    enemyRB.simulated = false;
                    spriteRenderer.color = Color.gray;
                }
                else
                {
                    enemyRB.simulated = true;
                    spriteRenderer.color = Color.white;
                }
            }
        }
    }
}