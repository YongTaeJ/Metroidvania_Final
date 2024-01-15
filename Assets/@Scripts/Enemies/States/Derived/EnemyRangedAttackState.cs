using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyRangedAttackState : EnemyAttackState
{
    private GameObject _bulletPrefab;
    public EnemyRangedAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _bulletPrefab = (GameObject) Resources.Load("Enemies/Bullets/EnemyBullet");
    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        FireBullet();
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
