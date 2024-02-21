using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHEntrySet : MonoBehaviour
{
    private Animator _animator;
    private VHBossRoom _bossRoom;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Initialize(VHBossRoom bossRoom)
    {
        _bossRoom = bossRoom;
    }

    public void Jump()
    {
        _animator.SetTrigger(AnimatorHash.Jump);
    }

    public void Animation_JumpEnd()
    {
        transform.position += Vector3.left * 3f * 1.7f;
    }

    public void Taunt()
    {
        _animator.SetTrigger("Taunt");
    }

    public void Animation_TauntEnd()
    {
        _bossRoom.BattleStart();
        Destroy(gameObject);
    }
}
