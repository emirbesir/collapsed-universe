using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private float damageInterval = 1f; // Time between damage
    [SerializeField] private int damageAmount = 1; // Amount of damage dealt

    private Coroutine damageCoroutine;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                damageCoroutine = StartCoroutine(DealDamageOverTime(playerHealth));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator DealDamageOverTime(PlayerHealth playerHealth)
    {
        while (true)
        {
            playerHealth.TakeDamage(damageAmount);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
