using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHDeadState : EnemyDeadState
{
    private VHBossRoom _bossRoom;
    public VHDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _bossRoom = GameObject.Find("VHBossRoom").GetComponent<VHBossRoom>();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        TimerManager.Instance.StartTimer(2.5f, () => SFX.Instance.PlayOneShot("VillageHeadDeadSound"));
    }

    public override void OnStateStay()
    {
        if(_isDeadEnded)
        {
            _bossRoom.GetDeadLocation(_stateMachine.transform.position);
            _bossRoom.GetDropTableIndex(_stateMachine.EnemyData.DropTableIndex);
            _bossRoom.OnBossDead();
            MonoBehaviour.Destroy(_stateMachine.gameObject);
        }
    }
}
