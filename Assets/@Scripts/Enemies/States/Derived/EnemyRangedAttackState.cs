using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyRangedAttackState : EnemyAttackState
{
    private GameObject _bulletPrefab;
    private PlayerFinder _playerFinder;
    public EnemyRangedAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _bulletPrefab = Resources.Load<GameObject>("Enemies/Bullets/EnemyBullet");
        _playerFinder = stateMachine.PlayerFinder;
        stateMachine.EventReceiver.OnBulletFire += FireBullet;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
    private void FireBullet()
    {
        Vector3 myPos = _attackPivot.position;

        EnemyBullet bullet = 
        GameObject.Instantiate(_bulletPrefab, myPos, quaternion.identity)
        .GetComponent<EnemyBullet>();

        Vector3 direction = (_playerFinder.CurrentTransform.position - myPos).normalized;

        bullet.InitDirection(direction);
    }


}
