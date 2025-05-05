using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollingCredits : MonoBehaviour
{
    public float scrollSpeed = 50f;  // Speed at which the credits scroll
    public float startY = -800f;     // Starting position (off-screen bottom)
    public float endY = 800f;        // Ending position (off-screen top)

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        // Set the initial position
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startY);
    }

    void Update()
    {
        // Move the credits panel upward
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

        // If it's gone off screen
        if (rectTransform.anchoredPosition.y > endY)
        {
            SceneManager.LoadScene(0); 
        }
    }
}