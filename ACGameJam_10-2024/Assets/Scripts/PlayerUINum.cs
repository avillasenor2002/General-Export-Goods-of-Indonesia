using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUINum: MonoBehaviour
{
    public BallMovement ballMovement; // Reference to BallMovement script
    public TextMeshProUGUI hpText;    // Reference to the TextMeshPro UI element

    private void Start()
    {
        if (ballMovement == null)
        {
            Debug.LogError("BallMovement script is not assigned.");
        }

        if (hpText == null)
        {
            Debug.LogError("TextMeshPro UI element is not assigned.");
        }
    }

    private void Update()
    {
        if (ballMovement != null && hpText != null)
        {
            hpText.text = "Lv. " + ballMovement.playerHealth.ToString();
        }
    }
}
