using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float _speed = 5f;

    public float _jumpPower = 10f;
    public int _maxJump = 1;
    public int _jumpCount;

    private Vector2 _moveInput;
    private TouchingDirections _touchingDirections;

    public float MoveSpeed { get
        {
            if(IsWalking && !_touchingDirections.IsWall)
            {
                return _speed;
            }
            else
            {
                return 0;
            }
        } }

    private bool _isWalking = false;

    public bool IsWalking { get { return _isWalking; }
        private set 
        {
            _isWalking = value;
            _animator.SetBool(AnimatorHash.Walk, value);
        }
    }

    public bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_moveInput.x * MoveSpeed, _rigidbody.velocity.y);
        _animator.SetFloat(AnimatorHash.yVelocity, _rigidbody.velocity.y);
        GroundCheck();
    }

    public void Move(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        IsWalking = _moveInput != Vector2.zero;

        Filp(_moveInput);
    }

    private void Filp(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && !IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (_jumpCount > 0)
        {
            if (context.performed && _touchingDirections.IsGrounded)
            {
                _animator.SetTrigger(AnimatorHash.Jump);
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower);
                _jumpCount--;
            }
            else if (context.canceled && _touchingDirections.IsGrounded)
            {
                _animator.SetTrigger(AnimatorHash.Jump);
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
                _jumpCount--;
            }
        }
    }

    private void GroundCheck()
    {
        if(_touchingDirections.IsGrounded == true)
        {
            _jumpCount = _maxJump;
        }
    }
}
