using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public LayerMask GroundLayerMask;

    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    private Collider2D _touchingCol;
    private Animator _animator;

    //private RaycastHit2D[] groundHit = new RaycastHit2D[5];
    //private RaycastHit2D[] wallHit = new RaycastHit2D[5];
    //private RaycastHit2D[] ceilingHit = new RaycastHit2D[5];

    private bool _isGrounded;

    public bool IsGrounded { get
        {
            return _isGrounded;
        } 
        private set
        {
            if (_isGrounded != value) // 이전 상태와 변경된 상태가 다를 때에만 실행
            {
                _isGrounded = value;
                _animator.SetBool(AnimatorHash.IsGrounded, value);
                if (value) // IsGrounded가 true일 때만 랜드 이펙트 호출
                {
                    LandEffect();
                    SFX.Instance.PlayOneShot(ResourceManager.Instance.GetAudioClip("Land"));
                }
            }
        }
    }

    private bool _isWall;
    private Vector2 _wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    public bool IsWall
    {
        get
        {
            return _isWall;
        }
        private set
        {
            _isWall = value;
            _animator.SetBool(AnimatorHash.IsWall, value);
        }
    }

    private bool _isCeiling;
    public bool IsCeiling
    {
        get
        {
            return _isCeiling;
        }
        private set
        {
            _isCeiling = value;
        }
    }

    private void Awake()
    {
        _touchingCol = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        castFilter.layerMask = GroundLayerMask;
    }

    private void Update()
    {
        CheckGrounded();
        CheckWall();
        CheckCeiling();
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(_touchingCol.bounds.center, Vector2.down, groundDistance + _touchingCol.bounds.extents.y, GroundLayerMask);
        IsGrounded = hit.collider != null;
    }

    private void CheckWall()
    {
        Vector2 direction = _wallCheckDirection;
        RaycastHit2D hit = Physics2D.Raycast(_touchingCol.bounds.center, direction, wallDistance + _touchingCol.bounds.extents.x, GroundLayerMask);
        IsWall = hit.collider != null;
    }

    private void CheckCeiling()
    {
        RaycastHit2D hit = Physics2D.Raycast(_touchingCol.bounds.center, Vector2.up, ceilingDistance + _touchingCol.bounds.extents.y, GroundLayerMask);
        IsCeiling = hit.collider != null;
    }


    public void LandEffect()
    {
        GameObject landEffect = ResourceManager.Instance.InstantiatePrefab("LandEffect", pooling: true);
        Vector2 point = new Vector2(transform.position.x, transform.position.y - 0.9f);
        landEffect.transform.position = point;
    }
}
