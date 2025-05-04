using UnityEngine;

public class FallToVoid : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.EndGame();
        }
    }
}
