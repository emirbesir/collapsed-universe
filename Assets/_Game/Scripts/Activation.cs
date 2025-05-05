using UnityEngine;
using UnityEngine.SceneManagement;

public class Activation : MonoBehaviour
{
    [SerializeField] private GameObject _keyboardKeyPopup;
    [SerializeField] private GameObject _keyItemInInventory;
    [SerializeField] private GameObject _VFX;
    [SerializeField] private GameObject fan;
    private bool isFanActive = false;


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
            _VFX.SetActive(true);
            fan.GetComponent<Fan>().enabled = true;
            fan.GetComponent<BoxCollider2D>().enabled = true;
            isFanActive = true;
            _keyboardKeyPopup.SetActive(false);
            Destroy(gameObject);
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
