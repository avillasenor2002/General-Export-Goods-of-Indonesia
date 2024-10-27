using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallCountChecker : MonoBehaviour
{
    public GameObject warningText; // Reference to the UI element to display when count is below 1
    private int ballCount;

    private void Start()
    {
        if (warningText == null)
        {
            Debug.LogError("Warning Text UI element is not assigned.");
        }
        else
        {
            warningText.gameObject.SetActive(false); // Hide the UI element initially
        }
    }

    private void Update()
    {
        // Count how many GameObjects currently have the BallMovement component
        ballCount = FindObjectsOfType<BallMovement>().Length;

        // Show warning UI element if ballCount is less than 1
        if (ballCount < 1)
        {
            warningText.gameObject.SetActive(true);
        }
        else
        {
            warningText.gameObject.SetActive(false);
        }
    }
}
