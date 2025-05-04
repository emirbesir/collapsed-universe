using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject _keyboardKeyPopup;
    private void Start()
    {
        // Ensure the popup is inactive at the start
        if (_keyboardKeyPopup != null)
        {
            _keyboardKeyPopup.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _keyboardKeyPopup.activeSelf)
        {
            // Load the next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _keyboardKeyPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _keyboardKeyPopup.SetActive(false);
        }
    }
}
