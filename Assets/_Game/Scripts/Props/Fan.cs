using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float _force = 10f;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
        }
    }

}
