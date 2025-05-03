using System.Collections;
using UnityEngine;


public class PressurePlate : MonoBehaviour
{
    [Header("Pressure Plate Settings")]
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private bool isActive = false;

    [Header("Visual Settings")]
    [SerializeField] private SpriteRenderer pressurePlateRend;

    private Color activeColor = Color.green;
    private Color inactiveColor = Color.red;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If pressure plate is not active and triggered, activate the object
        if (!isActive)
        {
            ActivateObject();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If pressure plate is active and exited, deactivate the object
        if (isActive)
        {
            DeactivateObject();
        }
    }

    // Activate the object and change the color of the pressure plate
    private void ActivateObject()
    {
        if (objectToActivate != null)
        {
            isActive = true;
            pressurePlateRend.color = activeColor;
            objectToActivate.SetActive(true);
        }
    }

    // Deactivate the object and change the color of the pressure plate
    private void DeactivateObject()
    {
        if (objectToActivate != null)
        {
            isActive = false;
            pressurePlateRend.color = inactiveColor;
            objectToActivate.SetActive(false);
        }
    }
}
