using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private float originScreenY = 0.5f;
    private bool isCameraMove = false; //카메라 움직임과 임펄스가 동시에 일어나면 m_Screen이 이상한 수치에 고정됨

    public override bool Initialize()
    {
        return base.Initialize();
    }

    public void CameraShake(CinemachineImpulseSource impulseSource, float shakeForce)
    {
        if (!isCameraMove)
        {
            impulseSource.GenerateImpulseWithForce(shakeForce);
        }
    }

    public void GetCamera(CinemachineVirtualCamera camera)
    {
        _virtualCamera = camera;
    }

    public void MoveCamera(Vector2 direction)
    {
        isCameraMove = true;

        CinemachineFramingTransposer framingTransposer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

            if (framingTransposer != null)
            {
                float screenY = framingTransposer.m_ScreenY;

                if (originScreenY == 0f)
                    originScreenY = screenY;

                float moveAmountY = direction.y * 0.3f;
                screenY += moveAmountY;
                screenY = Mathf.Clamp01(screenY);
                framingTransposer.m_ScreenY = screenY;
            }
    }

    private IEnumerator ResetCameraMove()
    {
        yield return new WaitForSeconds(0.1f); //돌아올 약간의 텀 시간 기다림
        isCameraMove = false;
    }

    public void ResetCamera()
    {
        CinemachineFramingTransposer framingTransposer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        if (framingTransposer != null)
        {
            framingTransposer.m_ScreenY = originScreenY;
        }
        StartCoroutine(ResetCameraMove());
    }
}
