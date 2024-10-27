using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyUINum : MonoBehaviour
{
    public EnemyScript enemyScript; // Reference to the EnemyScript
    public TextMeshProUGUI healthText; // Reference to the TextMeshPro element for displaying HP

    void Start()
    {
        if (enemyScript == null)
        {
            Debug.LogWarning("EnemyScript is not assigned in EnemyHealthMonitor.");
        }

        if (healthText == null)
        {
            Debug.LogWarning("TextMeshProUGUI component is not assigned in EnemyHealthMonitor.");
        }
    }

    void Update()
    {
        if (enemyScript != null && healthText != null)
        {
            healthText.text = $"{enemyScript.currentHealth}";
        }
    }
}
