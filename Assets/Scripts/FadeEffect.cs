using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The sprite to apply fade effect to")]
    public GameObject slowMotionSprite;
    [Tooltip("Minimum alpha value during pulsing")]
    [Range(0f, 1f)] public float minAlpha = 0.01f;
    [Tooltip("Maximum alpha value during pulsing")]
    [Range(0f, 1f)] public float maxAlpha = 0.03f;
    [Tooltip("Speed of the fade pulsing effect")]
    public float fadeSpeed = 3f;
    [Tooltip("Speed of fade out when stopping")]
    public float fadeOutSpeed = 5f;

    private float alpha = 0f;
    private SpriteRenderer spriteRenderer;
    public bool isFading = false;

    void Start()
    {
        if (slowMotionSprite == null)
        {
            Debug.LogError("SlowMotionSprite not assigned in FadeEffect!");
            return;
        }

        spriteRenderer = slowMotionSprite.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on SlowMotionSprite!");
        }

        // Initialize with zero alpha
        SetAlpha(0f);
    }

    void Update()
    {
        if (isFading)
        {
            UpdateFade();
        }
        else if (alpha > 0f)
        {
            UpdateFadeOut();
        }
    }

    private void UpdateFade()
    {
        // Calculate pulsing alpha using sine wave
        float normalizedSine = (Mathf.Sin(Time.unscaledTime * fadeSpeed)) * 0.1f; // 0-1 range
        alpha = Mathf.Lerp(minAlpha, maxAlpha, normalizedSine);
        SetAlpha(alpha);
    }

    private void UpdateFadeOut()
    {
        alpha = Mathf.MoveTowards(alpha, 0f, Time.unscaledDeltaTime);
        SetAlpha(alpha);
    }

    private void SetAlpha(float newAlpha)
    {
        if (spriteRenderer != null)
        {
            Color newColor = spriteRenderer.color;
            newColor.a = newAlpha;
            spriteRenderer.color = newColor;
        }
    }

    public void StartFade()
    {
        isFading = true;
    }

    public void Stop()
    {
        isFading = false;
    }

    // For external control
    public void SetFadeState(bool shouldFade)
    {
        isFading = shouldFade;
    }
}