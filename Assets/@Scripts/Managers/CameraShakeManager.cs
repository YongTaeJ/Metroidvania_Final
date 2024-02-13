using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeManager : Singleton<CameraShakeManager>
{
    public override bool Initialize()
    {
        return base.Initialize();
    }

    public void CameraShake(CinemachineImpulseSource impulseSource, float shakeForce)
    {
        impulseSource.GenerateImpulseWithForce(shakeForce);
    }
}
