using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AlexCursorShake : MonoBehaviour
{
    public Texture2D defaultCursorImage;
    public Texture2D enemyCursorImage;
    public Texture2D playerCursorImage;
    private AudioSource audioSource;
    private AudioClip sound;
    private float volume;
    public AlexSFKeys alexSFKeys;
     // Adjust the offset values as needed
    public Vector3 cursorOffset = new Vector3(10f, -10f, 0f);
    public float shakeIntensity = 5f;
    public float shakeDuration = 0.2f;
    public AnimationCurve shakeCurve;

    private Image cursorImage;
    private Vector3 originalPosition;
    private bool isShaking = false;
    private bool isHoldingWall = false;

    void Awake()
    {
        Cursor.visible = false;
        // Get the UI Image component
        cursorImage = GetComponent<Image>();
        if (cursorImage != null)
        {
            cursorImage.sprite = Sprite.Create(defaultCursorImage, new Rect(0, 0, defaultCursorImage.width, defaultCursorImage.height), Vector2.zero);
        }
    }

    void Update()
    {
        if (!isShaking) transform.position = Input.mousePosition + cursorOffset;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (Input.GetMouseButtonDown(0))
        {

            if (hit.collider != null && hit.collider.CompareTag("Wall"))
            {
                isHoldingWall = true;
            }
        }

        if (Input.GetMouseButton(0) && isHoldingWall)
        {
            cursorImage.sprite = Sprite.Create(playerCursorImage, new Rect(0, 0, playerCursorImage.width, playerCursorImage.height), Vector2.zero);
        }
        else
        {
            cursorImage.sprite = Sprite.Create(defaultCursorImage, new Rect(0, 0, defaultCursorImage.width, defaultCursorImage.height), Vector2.zero);
            isHoldingWall = false;
        }

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            // Change cursor to enemy image
            cursorImage.sprite = Sprite.Create(enemyCursorImage, new Rect(0, 0, enemyCursorImage.width, enemyCursorImage.height), Vector2.zero);
            if (Input.GetMouseButtonDown(0) && !isShaking )
            {
                StartCoroutine(ShakeCursor());
                alexSFKeys.KeySound();
            }
        }
        else
        {
            // Revert to default cursor image
            cursorImage.sprite = Sprite.Create(defaultCursorImage, new Rect(0, 0, defaultCursorImage.width, defaultCursorImage.height), Vector2.zero);
        }


    }

    IEnumerator ShakeCursor()
    {
        isShaking = true;
        float elapsedTime = 0f;
        originalPosition = Input.mousePosition;

        while (elapsedTime < shakeDuration)
        {
            Vector3 shakeOffset = new Vector3(
                Random.Range(-shakeIntensity, shakeIntensity),
                Random.Range(-shakeIntensity, shakeIntensity),
                0f
            );

            float strength = shakeCurve.Evaluate(elapsedTime / shakeDuration);
            transform.position = originalPosition + shakeOffset * strength + cursorOffset;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition + cursorOffset;
        isShaking = false;
    }
}