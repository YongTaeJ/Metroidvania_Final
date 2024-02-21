using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSEventSet : MonoBehaviour
{
    #region variables
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    public void Jump()
    {
        _animator.SetTrigger(AnimatorHash.Jump);
    }

    public void Talk()
    {
        _animator.SetTrigger(AnimatorHash.Walk);
    }

    public float Up()
    {
        float time = 1;

        Jump();
        transform.DOMoveY(10, time);

        return time;
    }
}
