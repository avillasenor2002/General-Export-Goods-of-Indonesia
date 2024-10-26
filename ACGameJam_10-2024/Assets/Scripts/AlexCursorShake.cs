using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AlexCursorShake : MonoBehaviour
{
    public float shakeIntensity = 5f;
    public float shakeDuration = 0.2f;
    public AnimationCurve shakeCurve;

    private Vector3 originalPosition;
    private bool isShaking = false;

    void Awake()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (!isShaking) transform.position = Input.mousePosition;
        //Start shaking on left mouse click, test only
        if (Input.GetMouseButtonDown(0) && !isShaking)
        {
            StartCoroutine(ShakeCursor());
        }
    }

    IEnumerator ShakeCursor()
    {
        isShaking = true;
        float elapsedTime = 0f;
        originalPosition = Input.mousePosition;

        while (elapsedTime < shakeDuration)
        {
            //Generate random shake offset
            Vector3 shakeOffset = new Vector3(
                Random.Range(-shakeIntensity, shakeIntensity),
                Random.Range(-shakeIntensity, shakeIntensity),
                0f
            );

            float strength = shakeCurve.Evaluate(elapsedTime / shakeDuration);
            transform.position = originalPosition + shakeOffset * strength;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Reset position after shaking
        transform.position = originalPosition;
        isShaking = false;
    }
}