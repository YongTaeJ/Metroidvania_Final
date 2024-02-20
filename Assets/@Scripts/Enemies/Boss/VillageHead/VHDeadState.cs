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

    public override void OnStateStay()
    {
        if(_isDeadEnded)
        {
            DropManager.Instance.DropItem(_stateMachine.EnemyData.DropTableIndex, _spriteTransform.position);
            _bossRoom.GetDeadLocation(_stateMachine.transform.position);
            _bossRoom.OnBossDead();
            MonoBehaviour.Destroy(_stateMachine.gameObject);
        }
    }
}
