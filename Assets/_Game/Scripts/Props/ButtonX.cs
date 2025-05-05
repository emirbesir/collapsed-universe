using System.Collections;
using UnityEngine;


public class ButtonX : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private GameObject[] objectsToActivate;
    [SerializeField] private bool isActive = false;

    [Header("Visual Settings")]
    [SerializeField] private SpriteRenderer buttonRend;

    private Color activeColor = Color.green;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If pressure plate is not active and triggered, activate the object
        if (!isActive)
        {
            ActivateObjects();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isActive)
        {
            isActive = false;
        }
    }


    // Activate the object and change the color of the pressure plate
    private void ActivateObjects()
    {
        foreach (GameObject objectToActivate in objectsToActivate)
        {
            if (objectToActivate != null)
            {
                isActive = true;
                buttonRend.color = activeColor;
                objectToActivate.SetActive(true);
            }
        }
    }
}
