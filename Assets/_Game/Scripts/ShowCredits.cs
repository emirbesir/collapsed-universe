using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowCredits : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private float fadeTime = 2f;

    private bool isDisplaying = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDisplaying)
            StopAllCoroutines();

        StartCoroutine(DisplayCredits());
    }
    private IEnumerator DisplayCredits()
    {
        isDisplaying = true;

        // Show panel and set text
        creditsPanel.SetActive(true);

        CanvasGroup canvasGroup = creditsPanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;

        if (canvasGroup != null)
        {
            // Fade in effect
            float elapsedTime = 0f;
            while (elapsedTime < fadeTime)
            {
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 1f;
        }

        yield return null;
    }
}
