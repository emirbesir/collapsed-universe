using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody2D _rb;
    private Vector2 _movementInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get normalized input from the player
        _movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Move the player based on input
    private void Move()
    {
        _rb.MovePosition(_rb.position + _moveSpeed * Time.deltaTime * _movementInput);
    }
}
