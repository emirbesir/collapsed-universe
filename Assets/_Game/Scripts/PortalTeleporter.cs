using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PortalTeleporter : MonoBehaviour
{
    // The portal to teleport to
    [SerializeField] private Transform destination;

    // Static bool flag to prevent teleport loops
    private static bool isTeleporting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTeleporting && destination != null)
        {
            StartCoroutine(Teleport(other.gameObject));
        }
    }

    // Teleport the object to the destination
    private IEnumerator Teleport(GameObject objectToTeleport)
    {
        isTeleporting = true;

        Rigidbody2D rb = objectToTeleport.GetComponent<Rigidbody2D>();
        Vector2 originalVelocity = Vector2.zero;
        if (rb != null)
        {
            originalVelocity = rb.linearVelocity;
        }

        Vector2 exitPosition = destination.position;

        objectToTeleport.transform.position = exitPosition;

        if (rb != null)
        {
            rb.linearVelocity = originalVelocity;
        }

        yield return new WaitForSeconds(0.5f);

        isTeleporting = false;
    }
}