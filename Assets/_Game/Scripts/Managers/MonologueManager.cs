using System.Collections;
using UnityEngine;
using TMPro; // Make sure TextMeshPro is imported in your project

public class MonologueManager : MonoBehaviour
{
    [SerializeField] private GameObject monologuePanel;
    [SerializeField] private TextMeshProUGUI monologueText;
    [SerializeField] private float displayTime = 3f;
    [SerializeField] private float fadeTime = 1f;

    private bool isDisplaying = false;

    private void Start()
    {
        if (monologuePanel != null)
            monologuePanel.SetActive(false);
    }

    public void ShowMonologue(string text)
    {
        if (isDisplaying)
            StopAllCoroutines();

        StartCoroutine(DisplayMonologue(text));
    }

    private IEnumerator DisplayMonologue(string text)
    {
        isDisplaying = true;

        // Show panel and set text
        monologuePanel.SetActive(true);
        monologueText.text = text;

        // Wait for display time
        yield return new WaitForSeconds(displayTime);

        // Optional: Fade out effect
        CanvasGroup canvasGroup = monologuePanel.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeTime)
            {
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 0f;
        }

        monologuePanel.SetActive(false);
        isDisplaying = false;
    }
}