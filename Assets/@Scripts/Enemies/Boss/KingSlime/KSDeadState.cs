using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSDeadState : EnemyDeadState
{
    private KSBossRoom _bossRoom;
    public KSDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _bossRoom = GameObject.Find("KSBossRoom").GetComponent<KSBossRoom>();
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
