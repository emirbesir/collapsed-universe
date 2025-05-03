using System.Collections;
using UnityEngine;


public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private bool isActive = false;
    [SerializeField] private SpriteRenderer pressurePlateRend;

    private Color activeColor = Color.green;
    private Color inactiveColor = Color.red;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive)
        {
            ActivateObject();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isActive)
        {
            DeactivateObject();
        }
    }

    private void ActivateObject()
    {
        if (objectToActivate != null)
        {
            isActive = true;
            pressurePlateRend.color = activeColor;
            objectToActivate.SetActive(true);
        }
    }

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
