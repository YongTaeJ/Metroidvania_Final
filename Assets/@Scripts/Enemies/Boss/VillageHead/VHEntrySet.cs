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
        TimerManager.Instance.StartTimer(0.5f, () => SFX.Instance.PlayOneShot("EnemyAttackSound", 0.5f));
        TimerManager.Instance.StartTimer(1.05f, () => SFX.Instance.PlayOneShot("EnemyAttackSound", 0.5f));
        _animator.SetTrigger(AnimatorHash.Jump);
    }

    public void Animation_JumpEnd()
    {
        transform.position += Vector3.left * 3f * 1.7f;
    }

    public void Taunt()
    {
        TimerManager.Instance.StartTimer(0.7f, () => SFX.Instance.PlayOneShot("VillageHeadTauntSound", 0.5f));
        _animator.SetTrigger("Taunt");
    }

    public void Animation_TauntEnd()
    {
        _bossRoom.BattleStart();
        Destroy(gameObject);
    }
}
