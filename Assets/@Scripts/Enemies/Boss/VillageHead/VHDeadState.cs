using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHDeadState : EnemyDeadState
{
    private BossRoom _bossRoom;
    public VHDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _bossRoom = GameObject.Find("VHBossRoom").GetComponent<BossRoom>();
    }

    public override void OnStateStay()
    {
        if(_isDeadEnded)
        {
            DropManager.Instance.DropItem(_stateMachine.EnemyData.DropTableIndex, _spriteTransform.position);
            GameObject.Destroy(_stateMachine.gameObject);
            _bossRoom.OpenDoor();
        }
    }
}
