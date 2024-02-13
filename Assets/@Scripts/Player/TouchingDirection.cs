using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    //public float ceilingDistance = 0.05f;

    private CapsuleCollider2D _touchingCol;
    private Animator _animator;

    private RaycastHit2D[] groundHit = new RaycastHit2D[5];
    private RaycastHit2D[] wallHit = new RaycastHit2D[5];
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

    //private bool _isCeiling;

    //public bool IsCeiling
    //{
    //    get
    //    {
    //        return _isCeiling;
    //    }
    //    private set
    //    {
    //        _isCeiling = value;
    //        _animator.SetBool(AnimatorHash.IsCeiling, value);
    //    }
    //}

    public LayerMask groundLayerMask;

    private void Awake()
    {
        _touchingCol = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        castFilter.layerMask = groundLayerMask;

        IsGrounded = _touchingCol.Cast(Vector2.down, castFilter, groundHit, groundDistance) > 0;
        IsWall = _touchingCol.Cast(_wallCheckDirection, castFilter, wallHit, wallDistance) > 0;
        //IsCeiling = _touchingCol.Cast(Vector2.up, castFilter, ceilingHit, ceilingDistance) > 0;
    }

    // 임시
    private GameObject landEffectPrefab;
    public void LandEffect()
    {
        landEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/LandEffect");
        GameObject landEffect = PoolManager.Instance.Pop(landEffectPrefab);
        Vector2 point = new Vector2(transform.position.x, transform.position.y - 0.9f);
        landEffect.transform.position = point;
    }
}
