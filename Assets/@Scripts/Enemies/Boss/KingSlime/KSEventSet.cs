using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSEventSet : MonoBehaviour
{
    #region variables
    private Animator _animator;
    #endregion

    private void Awake()
    {
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

        SFX.Instance.PlayOneShot("SummonRockSound");
        Jump();
        transform.DOMoveY(10, time);

        return time;
    }
}
