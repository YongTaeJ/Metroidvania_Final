using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHEndSet : MonoBehaviour
{
    #region variables
    private Animator _effectAnimator;
    private SpriteRenderer _effectRenderer;
    #endregion
    private void Awake()
    {
        _effectAnimator = transform.Find("EffectSprite").GetComponent<Animator>();
        _effectRenderer = transform.Find("EffectSprite").GetComponent<SpriteRenderer>();
    }

    public void TriggerEffect()
    {
        _effectRenderer.sortingOrder = 2;
        _effectAnimator.SetTrigger(AnimatorHash.Dead);
        Invoke("DestroyMySelf", 0.5f);
    }

    public void DestroyMySelf()
    {
        Destroy(gameObject);
    }


}
