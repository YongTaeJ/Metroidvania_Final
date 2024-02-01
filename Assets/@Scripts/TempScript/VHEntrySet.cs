using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHEntrySet : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Jump()
    {
        _animator.SetTrigger(AnimatorHash.Jump);
    }

    public void Animation_JumpEnd()
    {
        transform.position += Vector3.left * 3;
    }

    public void Taunt()
    {
        _animator.SetTrigger("Taunt");
    }

    public void Animation_TauntEnd()
    {
        GameObject targetObj = Resources.Load<GameObject>("Enemies/Bosses/Boss_VillageHead");
        Instantiate(targetObj, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
