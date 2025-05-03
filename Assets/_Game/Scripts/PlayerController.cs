using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _sprintMultiplier = 1.25f;

    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    private float _groundCheckRadius = 0.1f;

    [Header("Wall Jump Settings")]
    [SerializeField] private float _wallJumpForceX = 4f;
    [SerializeField] private float _wallJumpForceY = 6f;
    [SerializeField] private float _wallJumpCooldown = 0.5f;
    [SerializeField] private float _wallSlidingSpeed = 1.5f; 
    [SerializeField] private Transform[] _wallCheck;
    private float _wallCheckRadius = 0.15f;
    private float _wallJumpCooldownTimer = 0f;
    private bool _isWallJumping = false;
    private bool _canWallJump = true;
    private float _wallJumpingDirection;
    private int _lastWallDirection = 0; // 0 = none, -1 = left, 1 = right

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

        // Decrease wall jump cooldown timer
        if (_wallJumpCooldownTimer > 0)
        {
            _wallJumpCooldownTimer -= Time.deltaTime;
        }

        _movementInputX = Input.GetAxisRaw("Horizontal");

        // Handle jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                NormalJump();
            }
            else if (CanWallJump())
            {
                WallJump();
            }
        }

        // Reset wall jumping state when grounded
        if (IsGrounded())
        {
            StopWallJumping();
            // Reset last wall direction when grounded to allow wall jumps again
            _lastWallDirection = 0;
        }
    }

    private void FixedUpdate()
    {
        if (_isWallJumping)
        {
            return;
        }

        // Handle wall sliding
        if (IsTouchingWall() && !IsGrounded() && _movementInputX != 0)
        {
            // Get wall direction
            int wallDirection = GetWallDirection();

            // Prevent moving into the wall
            if ((_movementInputX > 0 && wallDirection > 0) || (_movementInputX < 0 && wallDirection < 0))
            {
                _rb.linearVelocity = new Vector2(0, Mathf.Max(_rb.linearVelocity.y, -_wallSlidingSpeed));
            }
            else
            {
                Move();
            }
        }
        else
        {
            // Normal movement
            Move();
        }
    }

    // Check if player can perform a wall jump
    private bool CanWallJump()
    {
        if (!_canWallJump || _wallJumpCooldownTimer > 0 || IsGrounded())
            return false;

        int wallDirection = GetWallDirection();

        // Can't jump if not touching wall
        if (wallDirection == 0)
            return false;

        // Can't jump from the same wall twice in a row
        if (wallDirection == _lastWallDirection && _lastWallDirection != 0)
            return false;

        return true;
    }

    // Get the direction of the wall (-1 = left, 0 = none, 1 = right)
    private int GetWallDirection()
    {
        for (int i = 0; i < _wallCheck.Length; i++)
        {
            if (Physics2D.OverlapCircle(_wallCheck[i].position, _wallCheckRadius, _groundLayer))
            {
                return (i == 0) ? -1 : 1;
            }
        }
        return 0; // No wall contact
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

    // Normal jump from the ground
    private void NormalJump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
    }

    // Wall jump
    private void WallJump()
    {
        // Get the wall direction
        int wallDirection = GetWallDirection();

        // Skip if no wall or same wall as last jump
        if (wallDirection == 0)
            return;

        // Jump away from the wall
        _rb.linearVelocity = new Vector2(-wallDirection * _wallJumpForceX, _wallJumpForceY);

        // Set wall jumping state
        _isWallJumping = true;
        _wallJumpCooldownTimer = _wallJumpCooldown;
        _lastWallDirection = wallDirection;

        Invoke(nameof(StopWallJumping), 0.2f);
    }

    // Stop wall jumping state
    private void StopWallJumping()
    {
        _isWallJumping = false;
    }

    // Check if the player is grounded
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer) != null;
    }

    // Check if the player is touching a wall
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