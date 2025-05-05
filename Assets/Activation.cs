using UnityEngine;
using UnityEngine.SceneManagement;

public class Activation : MonoBehaviour
{
    [SerializeField] private GameObject _keyboardKeyPopup;
    [SerializeField] private GameObject _keyItemInInventory;
    [SerializeField] private Fan fan;
    private bool isFanActive = false;

    private void Awake()
    {
        fan = GetComponent<Fan>();
    }

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
        if (Input.GetKeyDown(KeyCode.E) && _keyboardKeyPopup.activeSelf && _keyItemInInventory.activeSelf)
        {
            _keyItemInInventory.SetActive(false);
            fan.enabled = true;
            isFanActive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFanActive)
        {
            _keyboardKeyPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFanActive)
        {
            _keyboardKeyPopup.SetActive(false);
        }
    }
}
