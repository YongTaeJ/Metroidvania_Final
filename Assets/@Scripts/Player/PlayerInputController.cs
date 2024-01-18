using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private bool _isFacingRight = true;
    private float _speed = 5f;
    private float _horizontal;

    //Jump
    private float _jumpPower = 10f;
    public int _maxJump = 1;
    public int _jumpCount;

    //Ground Check
    public Transform _groundCheckPos;
    public Vector2 _groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    private bool _isGrounded;

    //Wall Check
    public Transform _wallCheckPos;
    public Vector2 _wallCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask wallLayer;

    //Gravity
    public float _baseGravity = 2f;
    public float _maxFallSpeed = 10f;
    public float _fallSpeedMultiplier = 2f;

    //Wall Slide
    private float _wallSlideSpeed = 0f;
    private bool _isWallSliding;

    // WallJump
    private bool _isWallJumping;
    private float _wallJumpDirection;
    private float _wallJumpTime = 0.3f;
    private float _wallJumpTimer;
    private Vector2 _wallJumpPower = new Vector2(5f, 10f);

    // Dash
    private bool _canDash = true;
    private bool _isDashing;
    private float _dashPower = 15f;
    private float _dashTime = 0.2f;
    private int _maxDash = 1;
    public int _dashCount;
    private TrailRenderer _trailRenderer;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        GroundCheck();
        WallSlide();
        WallJump();

        if (!_isWallJumping && !_isDashing)
        {
            _rigidbody.velocity = new Vector2(_horizontal * _speed, _rigidbody.velocity.y);
            Flip();
        }

        if (!_isDashing)
        {
            Gravity();
        }

        _animator.SetFloat(AnimatorHash.yVelocity, _rigidbody.velocity.y);
        _animator.SetFloat(AnimatorHash.Walk, _rigidbody.velocity.magnitude);
        _animator.SetBool(AnimatorHash.WallSliding, _isWallSliding);
        
    }

    private void Gravity()
    {
        if(_rigidbody.velocity.y < 0)
        {
            _rigidbody.gravityScale = _baseGravity * _fallSpeedMultiplier;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Max(_rigidbody.velocity.y, -_maxFallSpeed));
        }
        else
        {
            _rigidbody.gravityScale = _baseGravity;
        }
    }

    private void WallSlide()
    {
        if(!_isGrounded & WallCheck() & _horizontal != 0)
        {
            _isWallSliding = true;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Max(_rigidbody.velocity.y, -_wallSlideSpeed));
        }
        else
        {
            _isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (_isWallSliding)
        {
            _isWallJumping = false;
            _wallJumpDirection = -transform.localScale.x;
            _wallJumpTimer = _wallJumpTime;

            CancelInvoke(nameof(CancelWallJump));
        }
        else if (_wallJumpTimer > 0f)
        {
            _wallJumpTimer -= Time.deltaTime;
        }
    }

    private void CancelWallJump()
    {
        _isWallJumping = false;
    }

    public void Move(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _animator.SetTrigger(AnimatorHash.Attack);

        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (_jumpCount > 0)
        {
            if (context.performed)
            {
                if(!_isWallJumping)
                {
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower);
                    _animator.SetTrigger(AnimatorHash.Jump);
                    _jumpCount--;
                }
            }
            else if (context.canceled)
            {
                if (!_isWallJumping)
                {
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
                    _animator.SetTrigger(AnimatorHash.Jump);
                    _jumpCount--;
                }
            }
        }

        //Wall Jump
        if(context.performed && _wallJumpTimer > 0f)
        {
            if (!_isWallJumping)
            {
                _isWallJumping = true;
                _rigidbody.velocity = new Vector2(_wallJumpDirection * _wallJumpPower.x, _wallJumpPower.y);
                _wallJumpTimer = 0f;
                _animator.SetTrigger(AnimatorHash.WallJump);
                if (transform.localScale.x != _wallJumpDirection)
                {
                    _isFacingRight = !_isFacingRight;
                    Vector3 Is = transform.localScale;
                    Is.x *= -1f;
                    transform.localScale = Is;
                }

                Invoke(nameof(CancelWallJump), _wallJumpTime + 0.1f);
            }
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && _canDash == true)
        {
            StartCoroutine(CoDash());
        }
    }

    private IEnumerator CoDash()
    {
        if(_dashCount > 0)
        {
            _animator.SetTrigger(AnimatorHash.Dash);
            _canDash = false;
            _isDashing = true;
            float originalGravity = _baseGravity;
            _rigidbody.gravityScale = 0f;
            _rigidbody.velocity = new Vector2(transform.localScale.x * _dashPower, 0f);
            _trailRenderer.emitting = true;
            yield return new WaitForSeconds(_dashTime);
            _trailRenderer.emitting = false;
            _rigidbody.gravityScale = originalGravity;
            _isDashing = false;
            _dashCount--;
            yield return new WaitForSeconds(0.1f);
            _canDash = true;
        }
    }

    private void GroundCheck()
    {
        if(Physics2D.OverlapBox(_groundCheckPos.position, _groundCheckSize,0, groundLayer))
        {
            _jumpCount = _maxJump;
            _dashCount = _maxDash;
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }

    private bool WallCheck()
    {
        return Physics2D.OverlapBox(_wallCheckPos.position, _wallCheckSize, 0, wallLayer);
    }

    private void Flip()
    {
        if(_isFacingRight && _horizontal < 0 || !_isFacingRight && _horizontal > 0)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 Is = transform.localScale;
            Is.x *= -1f;
            transform.localScale = Is;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(_groundCheckPos.position, _groundCheckSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_wallCheckPos.position, _wallCheckSize);
    }
}
