using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rb;
    private float _movementInputX;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _movementInputX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
    
    // Move the player based on input
    private void Move()
    {
        _rb.linearVelocity = new Vector2(_movementInputX * _moveSpeed, _rb.linearVelocity.y);
    }

    // Jump the player
    private void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce); 
    }

    // Check if the player is grounded
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.1f, _groundLayer) != null;
    }
}
