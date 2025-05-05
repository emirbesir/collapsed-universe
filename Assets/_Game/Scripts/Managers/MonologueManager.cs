using System.Collections;
using UnityEngine;
using TMPro; // Make sure TextMeshPro is imported in your project

public class MonologueManager : MonoBehaviour
{
    [SerializeField] private GameObject monologuePanel;
    [SerializeField] private TextMeshProUGUI monologueText;
    [SerializeField] private float displayTime = 5f;
    [SerializeField] private float fadeTime = 1f;

    private bool isDisplaying = false;
    
    // Singleton instance
    public static MonologueManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

        CanvasGroup canvasGroup = monologuePanel.GetComponent<CanvasGroup>();
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

        // Wait for display time
        yield return new WaitForSeconds(displayTime);

        // Fade out effect
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