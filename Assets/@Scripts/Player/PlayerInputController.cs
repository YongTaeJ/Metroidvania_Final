using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _speed = 5f;
    private float _jumpPower = 10f;
    private int _maxJump = 2;
    public int _jumpCount;
    private float _horizontalMovement;
    public Transform _groundCheckPos;
    public Vector2 _groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    [Header("Gravity")]
    public float _baseGravity = 2f;
    public float _maxFallSpeed = 10f;
    public float _fallSpeedMultiplier = 2f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rigidbody.velocity = new Vector2(_horizontalMovement * _speed, _rigidbody.velocity.y);
        GroundCheck();
        Gravity();
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

    public void Move(InputAction.CallbackContext context)
    {
        _horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (_jumpCount > 0)
        {
            if (context.performed)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower);
                _jumpCount--;
            }
            else if (context.canceled)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
            }
        }
    }

    private void GroundCheck()
    {
        if(Physics2D.OverlapBox(_groundCheckPos.position, _groundCheckSize,0, groundLayer))
        {
            _jumpCount = _maxJump;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(_groundCheckPos.position, _groundCheckSize);
    }
}
