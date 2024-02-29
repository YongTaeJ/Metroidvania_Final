using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyRangedAttackState : EnemyAttackState
{
    #region Fields
    private GameObject _bulletPrefab;
    private PlayerFinder _playerFinder;
    private int _bulletIndex;
    private int _damage;
    #endregion

    public EnemyRangedAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        _bulletIndex = stateMachine.EnemyData.BulletIndex;
        _bulletPrefab = Resources.Load<GameObject>("Enemies/Bullets/EnemyBullet" + _bulletIndex.ToString() );
        _playerFinder = stateMachine.PlayerFinder;
        _damage = stateMachine.EnemyData.Damage;
        stateMachine.EventReceiver.OnBulletFire += FireBullet;
    }

    #region IState
    public override void OnStateExit()
    {
        base.OnStateExit();
    }
    #endregion
    
    private void FireBullet()
    {
        if (_playerFinder.CurrentTransform)
        {
            PlayAttackSound();
            Vector3 myPos = _attackPivot.position;

            EnemyBullet bullet =
            GameObject.Instantiate(_bulletPrefab, myPos, quaternion.identity)
            .GetComponent<EnemyBullet>();

            Vector3 direction = (_playerFinder.CurrentTransform.position - myPos).normalized;

            bullet.Initialize(direction, _damage);
        }
    }


}
