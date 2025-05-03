using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _sprintMultiplier = 1.25f;

    [Header("Jump / Ground Check Settings")]
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform[] _wallCheck;
    private float _groundCheckRadius = 0.1f;
    private float _wallCheckRadius = 0.15f;

    private Rigidbody2D _rb;
    private float _movementInputX;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Disable input if the game is paused
        if (GameManager.Instance.IsPaused)
        {
            _movementInputX = 0f; // Stop movement
            return;
        }

        _movementInputX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || IsTouchingWall())
            {
                Jump();
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Move the player based on input
    private void Move()
    {
        float speed = _moveSpeed;

        // Check if the player is sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= _sprintMultiplier;
        }

        _rb.linearVelocity = new Vector2(_movementInputX * speed, _rb.linearVelocity.y);
    }

    // Jump the player
    private void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
    }

    // Check if the player is grounded
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer) != null;
    }

    private bool IsTouchingWall()
    {
        foreach (var wallCheck in _wallCheck)
        {
            if (Physics2D.OverlapCircle(wallCheck.position, _wallCheckRadius, _groundLayer) != null)
            {
                return true;
            }
        }
        return false;
    }
}

