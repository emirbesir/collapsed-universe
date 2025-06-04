using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Settings")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;

    [Header("Visual Settings")]
    [SerializeField] private SpriteRenderer playerVisual;
    [Header("Audio Settings")]
    [SerializeField] private AudioClip hurtSound;

    private AudioSource AudioSource;
    private Color hurtColor = Color.red;
    private Color originalColor;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }
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
        GameManager.Instance.EndGame();
        gameObject.SetActive(false);
    }

    // Coroutine to change the color of the player when hurt
    private IEnumerator HurtColor()
    {
        playerVisual.color = hurtColor;
        AudioSource.PlayOneShot(hurtSound);
        yield return new WaitForSeconds(0.5f);
        playerVisual.color = originalColor;
    }
}
