using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushParticle : MonoBehaviour
{
    private new ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void LateUpdate()
    {
        //  파티클이 모두 종료되었을 때
        if (!particleSystem.IsAlive())
        {
            PoolManager.Instance.Push(gameObject);
        }
    }
}
