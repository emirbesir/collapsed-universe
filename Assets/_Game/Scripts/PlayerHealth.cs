using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private SpriteRenderer playerVisual;
    private int currentHealth;
    
    private Color hurtColor = Color.red;
    private Color originalColor;

    private void Start()
    {
        currentHealth = maxHealth;
        originalColor = playerVisual.color;
    }

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

    private IEnumerator HurtColor()
    {
        playerVisual.color = hurtColor;
        yield return new WaitForSeconds(0.5f);
        playerVisual.color = originalColor;
    }
}
