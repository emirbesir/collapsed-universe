using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Settings")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;

    [Header("Visual Settings")]
    [SerializeField] private SpriteRenderer playerVisual;
    
    private Color hurtColor = Color.red;
    private Color originalColor;

    private void Start()
    {
        currentHealth = maxHealth;
        originalColor = playerVisual.color;
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(HurtColor());
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        gameObject.SetActive(false);
    }

    // Coroutine to change the color of the player when hurt
    private IEnumerator HurtColor()
    {
        playerVisual.color = hurtColor;
        yield return new WaitForSeconds(0.5f);
        playerVisual.color = originalColor;
    }
}
