using System.Collections;
using UnityEngine;

public class ShatterZone : MonoBehaviour
{
    [SerializeField] private GameObject shatterPanel;
    [SerializeField] private float displayTime = 2f;
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private bool playOnce = true;
    [SerializeField] private float shatterAlpha = 0.5f;

    private bool hasPlayed = false;
    private bool isDisplaying = false;

    private void Start()
    {
        if (shatterPanel != null)
            shatterPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playOnce && hasPlayed)
                return;

            if (isDisplaying)
                StopAllCoroutines();

            StartCoroutine(DisplayShatter());
            hasPlayed = true;
        }
    }

    private IEnumerator DisplayShatter()
    {
        isDisplaying = true;

        // Show panel and set text
        shatterPanel.SetActive(true);

        CanvasGroup canvasGroup = shatterPanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;

        if (canvasGroup != null)
        {
            // Fade in effect
            float elapsedTime = 0f;
            while (elapsedTime < fadeTime)
            {
                canvasGroup.alpha = Mathf.Lerp(0f, shatterAlpha, elapsedTime / fadeTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = shatterAlpha;
        }

        // Wait for display time
        yield return new WaitForSeconds(displayTime);

        // Fade out effect
        if (canvasGroup != null)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeTime)
            {
                canvasGroup.alpha = Mathf.Lerp(shatterAlpha, 0f, elapsedTime / fadeTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 0f;
        }

        shatterPanel.SetActive(false);
        isDisplaying = false;
    }
}