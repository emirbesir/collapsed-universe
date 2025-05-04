using UnityEngine;

public class MonologueTrigger : MonoBehaviour
{
    [SerializeField] private string monologueText;
    [SerializeField] private bool playOnce = true;

    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playOnce && hasPlayed)
                return;

            MonologueManager.Instance.ShowMonologue(monologueText);
            hasPlayed = true;
        }
    }
}