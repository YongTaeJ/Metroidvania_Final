using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSystem : MonoBehaviour
{
    private int _damage;

    public void Initialize(EnemyStateMachine stateMachine)
    {
        _damage = stateMachine.EnemyData.Damage;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<IDamagable>().GetDamaged(_damage, this.transform);
        }
    }
}
