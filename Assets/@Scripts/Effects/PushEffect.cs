using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEffect : MonoBehaviour
{
    private Animator animator;
    private float duration; // 애니메이션 재생 시간

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            // 애니메이션의 재생 시간을 가져옴
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            duration = clipInfo[0].clip.length;
        }

        Invoke("ReturnToPool", duration);
    }

    private void ReturnToPool()
    {
        ResourceManager.Instance.Destroy(this.gameObject);
    }
}
