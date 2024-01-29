using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadEffect : MonoBehaviour
{
    private void OnEffectEnded()
    {
        Destroy(gameObject);
    }
}
