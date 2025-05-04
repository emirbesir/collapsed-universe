using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;

    // Singleton instance
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Show the Game Over panel
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    // Toggle the Pause panel
    public void TogglePausePanel(bool isPaused)
    {
        pausePanel.SetActive(isPaused);
    }
}
