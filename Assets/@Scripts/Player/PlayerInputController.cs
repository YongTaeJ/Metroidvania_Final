using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerInputController : MonoBehaviour
{
    #region Properties

    public bool Iswalking
    {
        get
        {
            return _isWalking;
        }
        private set
        {
            _isWalking = value;
            _animator.SetBool(AnimatorHash.Walk, value);
        }
    }

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            _isFacingRight = value;
        }
    }

    public bool IsAttacking
    {
        get
        {
            return _isAttacking;
        }
        private set
        {
            _isAttacking = value;
            _animator.SetBool(AnimatorHash.IsAttacking, value);
        }
    }
    #endregion

    #region Fileds

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private TouchingDirection _touchingDirection;
    private Player _player;
    private float _speed = 10f;
    private Vector2 _moveInput;

    private bool _isWalking = false;
    private bool _isAttacking = false;
    private bool _isFirstAttack = true;
    private bool _isFacingRight = true;

    //Jump
    private float _jumpPower = 20f;
    public int _maxJump = 1;
    public int _jumpCount;

    //Gravity
    private float _baseGravity = 4f;
    private float _maxFallSpeed = 20f;
    private float _fallSpeedMultiplier = 2f;

    //Wall Slide
    private float _wallSlideSpeed = 0f;
    private bool _isWallSliding = false;
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
    private float _wallJumpTime = 0.25f;
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

    // Action

    public event Action OnInteraction;

    //임시

    private GameObject attackEffectPrefab;
    private GameObject attackEffect2Prefab;
    private GameObject wallSlideEffectPrefab;
    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _touchingDirection = GetComponent<TouchingDirection>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        WallSlide();
        WallJump();

        if (!_isWallJumping && !_isDashing && !_player.IsHit)
        {
            _rigidbody.velocity = new Vector2(_moveInput.x * _speed, _rigidbody.velocity.y);
            Flip();
        }

        if (!_isDashing)
        {
            Gravity();
        }

        _animator.SetFloat(AnimatorHash.yVelocity, _rigidbody.velocity.y);
    }

    #endregion

    #region Movement

    public void Move(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

        Iswalking = _moveInput != Vector2.zero;
    }

    /// <summary>
    /// 연출 전용 옵션
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Vector2 direction)
    {
        _moveInput = direction;
        Iswalking = _moveInput != Vector2.zero;
    }

    private void Flip()
    {
        if (_isAttacking == false)
        {
            if (_isFacingRight && _moveInput.x < 0 || !_isFacingRight && _moveInput.x > 0)
            {
                _isFacingRight = !_isFacingRight;
                Vector3 Is = transform.localScale;
                Is.x *= -1f;
                transform.localScale = Is;
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (enabled)
        {
            if (_jumpCount > 0)
            {
                if (context.started && _touchingDirection.IsGrounded && !_isDashing)
                {
                    if (!_isWallJumping)
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
            if (context.performed && _wallJumpTimer > 0f)
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
    }

    private bool isWallSlideEffect = false;

    private void WallSlide()
    {
        if (enabled)
        {
            if (!_touchingDirection.IsGrounded & _touchingDirection.IsWall & _moveInput.x != 0 && ItemManager.Instance.HasItem(ItemType.Equipment, 1))
            {
                IsWallSliding = true;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Max(_rigidbody.velocity.y, -_wallSlideSpeed));
                if (!isWallSlideEffect)
                {
                    isWallSlideEffect = true;
                    StartCoroutine(WallSlideEffect());
                }
            }
            else
            {
                IsWallSliding = false;
                isWallSlideEffect = false;
            }
        }
    }

    private IEnumerator WallSlideEffect()
    {
        while (isWallSlideEffect == true)
        {
            yield return new WaitForSeconds(0.1f);

            wallSlideEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/WallSlideEffect");
            GameObject wallSlideEffect = PoolManager.Instance.Pop(wallSlideEffectPrefab);

            float wallSlideDirection = IsFacingRight ? 0.25f : -0.25f;
            Vector2 point = new Vector2(transform.position.x + wallSlideDirection, transform.position.y + 1.2f);
            wallSlideEffect.transform.position = point;

            float wallSlideScale = IsFacingRight ? -1 : 1;
            Vector3 scale = new Vector3(wallSlideScale, 1, 1);
            wallSlideEffect.transform.localScale = scale;

            float wallSlideRotation = IsFacingRight ? 90 : 270;
            wallSlideEffect.transform.rotation = Quaternion.Euler(0, 0, wallSlideRotation);
        }
    }

    private void WallJump()
    {
        if (enabled)
        {
            if (IsWallSliding)
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
    }

    private void CancelWallJump()
    {
        _isWallJumping = false;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (enabled)
        {
            if (context.performed && !IsAttacking)
            {
                IsAttacking = true;
                if (_isFirstAttack)
                {
                    _animator.SetTrigger(AnimatorHash.Attack);
                    attackEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/AttackEffect_Temp");
                    GameObject attackEffect = PoolManager.Instance.Pop(attackEffectPrefab);

                    float attackDirection = IsFacingRight ? 1f : -1f;
                    Vector2 attackPoint = new Vector2(transform.position.x + (attackDirection * 1.5f), transform.position.y + 0.5f);

                    attackEffect.transform.SetParent(transform);
                    attackEffect.transform.localScale = new Vector2(1.5f, 1.5f);
                    attackEffect.transform.position = attackPoint;
                    _isFirstAttack = false;
                }
                else if (!_isFirstAttack)
                {
                    _animator.SetTrigger(AnimatorHash.Attack2);
                    attackEffect2Prefab = Resources.Load<GameObject>("Prefabs/Effects/AttackEffect_Temp_2");
                    GameObject attackEffect2 = PoolManager.Instance.Pop(attackEffect2Prefab);

                    float attackDirection = IsFacingRight ? 1f : -1f;
                    Vector2 attackPoint = new Vector2(transform.position.x + (attackDirection * 1.5f), transform.position.y + 0.1f);

                    attackEffect2.transform.SetParent(transform);
                    attackEffect2.transform.localScale = new Vector2(1.5f, 1.5f);
                    attackEffect2.transform.position = attackPoint;
                    _isFirstAttack = true;
                }
                StartCoroutine(ResetAttackAnimation());
            }
        }
    }

    // TODO 리펙토링 필요해 보임
    private IEnumerator ResetAttackAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        IsAttacking = false;
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (enabled)
        {
            if (context.performed && _canDash == true && ItemManager.Instance.HasItem(ItemType.Equipment, 0))
            {
                StartCoroutine(CoDash());
            }
        }
    }

    private IEnumerator CoDash()
    {
        if (_dashCount > 0)
        {
            _animator.SetTrigger(AnimatorHash.Dash);
            _canDash = false;
            _isDashing = true;
            _player.Invincible = true;
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

    public void Skill(InputAction.CallbackContext context)
    {
        if (enabled)
        {
            if (!Input.GetKey(KeyCode.DownArrow) && context.performed && ItemManager.Instance.HasItem(ItemType.Skill, 0))
            {
                _player._skills[0].Activate();
            }
            else
            {
                Debug.Log("아이템 없음");
            }

            if (Input.GetKey(KeyCode.DownArrow) && !_touchingDirection.IsGrounded && context.performed && ItemManager.Instance.HasItem(ItemType.Skill, 1))
            {
                _player._skills[1].Activate();
                _player.Invincible = true;
            }
            else
            {
                Debug.Log("아이템 없음");
            }
        }
    }

    public void Interaction(InputAction.CallbackContext context)
    {
        if (enabled && context.performed)
        {
            OnInteraction?.Invoke();
        }
    }

    private void Gravity()
    {
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.gravityScale = _baseGravity * _fallSpeedMultiplier;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Max(_rigidbody.velocity.y, -_maxFallSpeed));
        }
        else
        {
            _rigidbody.gravityScale = _baseGravity;
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

    #region UI Input

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UIManager.Instance.OpenPopupUI(PopupType.Status);
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //if 켜진 UI가 없다면
            UIManager.Instance.OpenPopupUI(PopupType.Pause);
            Time.timeScale = 0f;
            //else if 켜진 Popup이 있다면 UI SetActive(false)

        }
    }

    #endregion
}
