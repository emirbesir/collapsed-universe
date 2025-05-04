using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Game state variables
    private bool _isPaused = false;
    public bool IsPaused => _isPaused;


    // Singleton instance
    public static GameManager Instance { get; private set; }

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

    
    public void EndGame()
    {
        UIManager.Instance.ShowGameOver();
    }

    private void Update()
    {
        // Toggle pause menu when Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Pause or resume the game
    public void TogglePause()
    {
        _isPaused = !_isPaused;
        UIManager.Instance.TogglePausePanel(_isPaused);

        Time.timeScale = _isPaused ? 0f : 1f;
    }
}
