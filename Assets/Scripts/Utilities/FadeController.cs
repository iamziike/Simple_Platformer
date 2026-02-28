using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public sealed class FadeController : MonoBehaviour
{
    [SerializeField] public Image fadeOverlay { get; private set; }

    public static FadeController instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            fadeOverlay = GetComponentInChildren<Image>();
            fadeOverlay.enabled = false;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeOutFadeIn(float duration = 1f, System.Action onComplete = null)
    {
        fadeOverlay.enabled = true;
        float halfDuration = duration / 2f;

        FadeOut(halfDuration, () =>
        {
            onComplete?.Invoke();
            FadeIn(halfDuration, () =>
            {
                fadeOverlay.enabled = false;
            });
        });
    }

    public void FadeOut(float duration = 1f, System.Action onComplete = null)
    {
        StartCoroutine(Fade(0f, 1f, duration, onComplete));
    }

    public void FadeIn(float duration = 1f, System.Action onComplete = null)
    {
        StartCoroutine(Fade(1f, 0f, duration, onComplete));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration, System.Action onComplete)
    {
        float elapsed = 0f;
        Color color = fadeOverlay.color;
        color.a = startAlpha;
        fadeOverlay.color = color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            color.a = alpha;
            fadeOverlay.color = color;
            yield return null;
        }

        // Ensure the final alpha is set
        color.a = endAlpha;
        fadeOverlay.color = color;

        onComplete?.Invoke();
    }
}