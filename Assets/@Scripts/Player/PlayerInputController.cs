using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour, IDamagable
{
    #region Properties

    public int _maxHp = 100;
    public int _Hp;
    public int _damage = 5;

    #endregion

    #region Fileds

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private TouchingDirection _touchingDirection;
    private bool _isFacingRight = true;
    private float _speed = 10f;
    private Vector2 _moveInput;

    private bool _isWalking = false;
    public bool Iswalking {get
        {
            return _isWalking;
        }
        private set
        {
            _isWalking = value;
            _animator.SetBool(AnimatorHash.Walk, value);
        }
    }

    //Jump
    private float _jumpPower = 20f;
    public int _maxJump = 1;
    public int _jumpCount;

    ////Ground Check
    //public Transform _groundCheckPos;
    //public Vector2 _groundCheckSize = new Vector2(0.5f, 0.05f);
    //public LayerMask groundLayer;
    //private bool _isGrounded;

    ////Wall Check
    //public Transform _wallCheckPos;
    //public Vector2 _wallCheckSize = new Vector2(0.5f, 0.05f);
    //public LayerMask wallLayer;

    //Gravity
    public float _baseGravity = 2f;
    public float _maxFallSpeed = 10f;
    public float _fallSpeedMultiplier = 2f;

    //Wall Slide
    private float _wallSlideSpeed = 0f;
    private bool _isWallSliding;

    public bool IsWallSliding { get 
        {
            return _isWallSliding; 
        } 
        private set
        {
            _isWallSliding = value;
            _animator.SetBool(AnimatorHash.WallSliding, value);
        }
    }

    // WallJump
    private bool _isWallJumping;
    private float _wallJumpDirection;
    private float _wallJumpTime = 0.3f;
    private float _wallJumpTimer;
    private Vector2 _wallJumpPower = new Vector2(5f, 15f);

    // Dash
    private bool _canDash = true;
    private bool _isDashing;
    private float _dashPower = 15f;
    private float _dashTime = 0.2f;
    private int _maxDash = 1;
    public int _dashCount;
    private TrailRenderer _trailRenderer;

    // 무적시간
    public bool Invincible
    {
        get => _invincible;
        set
        {
            _invincible = value;
            if (_invincible)
            {
                if (_coInvincible != null) StopCoroutine(_coInvincible);
                _coInvincible = StartCoroutine(InvincibleTimer(_invincibilityTime));
            }
        }
    }

    public bool IsHit { get
        {
            return _animator.GetBool(AnimatorHash.Hurt);
        }
        private set
        {
            _animator.SetBool(AnimatorHash.Hurt, value);
        }
    }

    private bool _invincible = false;
    private Coroutine _coInvincible;
    private float _invincibilityTime = 1f;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _touchingDirection = GetComponent<TouchingDirection>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _animator = GetComponent<Animator>();
        _Hp = _maxHp;

    }

    private void FixedUpdate()
    {
        GroundCheck();
        WallSlide();
        WallJump();

        if (!_isWallJumping && !_isDashing && !IsHit)
        {
            _rigidbody.velocity = new Vector2(_moveInput.x * _speed, _rigidbody.velocity.y);
            Flip();
        }

        if (!_isDashing)
        {
            Gravity();
        }

        _animator.SetFloat(AnimatorHash.yVelocity, _rigidbody.velocity.y);
        _animator.SetBool(AnimatorHash.IsGrounded, _touchingDirection.IsGrounded);
    }

    #endregion

    #region Movement

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
        if (!_touchingDirection.IsGrounded & _touchingDirection.IsWall & _moveInput.x != 0)
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
        _moveInput = context.ReadValue<Vector2>();

        Iswalking = _moveInput != Vector2.zero;
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
            if (context.started && _touchingDirection.IsGrounded)
            {
                if(!_isWallJumping)
                {
                    _animator.SetTrigger(AnimatorHash.Jump);
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower);
                    _jumpCount--;
                }
            }
            else if (context.canceled)
            {
                if (!_isWallJumping)
                {
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
                    _jumpCount--;
                }
            }
        }

        //Wall Jump
        if(context.performed && _wallJumpTimer > 0f)
        {
            if (!_isWallJumping && _touchingDirection.IsWall)
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

    #endregion

    #region Check

    private void GroundCheck()
    {
        if (_touchingDirection.IsGrounded)
        {
            _jumpCount = _maxJump;
            _dashCount = _maxDash;
        }
    }

    #endregion

    private void Flip()
    {
        if(_isFacingRight && _moveInput.x < 0 || !_isFacingRight && _moveInput.x > 0)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 Is = transform.localScale;
            Is.x *= -1f;
            transform.localScale = Is;
        }
    }

    #region Attribute Method

    public void GetDamaged(int damage, Transform target)
    {
        if (Invincible == false)
        {
            Invincible = true;

            StartCoroutine(FlashPlayer());
            IsHit = true;
            _Hp -= damage;

            StartCoroutine(ResetHurtAnimation());
            StartCoroutine(Knockback(target));

            if (_Hp <= 0)
            {
                _Hp = 0;
                _animator.SetBool(AnimatorHash.Dead, true);
            }
        }
    }

    private IEnumerator Knockback(Transform target)
    {
        // 플레이어와 피해를 준 오브젝트 간의 상대적인 위치를 비교하여 방향 계산
        float direction = Mathf.Sign(target.position.x - transform.position.x);

        // 플레이어를 피해를 받은 방향으로 밀어냄
        Vector2 knockbackDirection = new Vector2(-direction, 1f).normalized;
        _rigidbody.velocity = knockbackDirection * 5f;

        yield return new WaitForSeconds(0.2f);

        _rigidbody.velocity = Vector2.zero;
    }

    private IEnumerator ResetHurtAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        IsHit = false;
    }

    private IEnumerator InvincibleTimer(float invincibilityTime)
    {
        yield return new WaitForSeconds(invincibilityTime);
        Invincible = false;
    }

    private IEnumerator FlashPlayer()
    {
        float flashSpeed = 0.1f;

        while (Invincible)
        {
            Color originalColor = GetComponent<SpriteRenderer>().color;
            Color flashColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.1f);
            GetComponent<SpriteRenderer>().color = flashColor;

            yield return new WaitForSeconds(flashSpeed);

            GetComponent<SpriteRenderer>().color = originalColor;

            yield return new WaitForSeconds(flashSpeed);
        }
    }

    #endregion

   
}
