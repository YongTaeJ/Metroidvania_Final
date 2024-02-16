using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] public CinemachineVirtualCamera _virtualCamera;
    private Transform _originalFollowTransform;
    public override bool Initialize()
    {
        return base.Initialize();
    }

    public void CameraShake(CinemachineImpulseSource impulseSource, float shakeForce)
    {
        impulseSource.GenerateImpulseWithForce(shakeForce);
    }

    public void GetCamera(CinemachineVirtualCamera camera)
    {
        _virtualCamera = camera;
    }

    public void MoveCamera(Vector2 direction)
    {
        CinemachineFramingTransposer framingTransposer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        if (framingTransposer != null)
        {
            // 현재 Screen Y 값을 가져옴
            float screenY = framingTransposer.m_ScreenY;

            // 원하는 방향으로 이동할 양을 설정
            float moveAmountY = direction.y * 0.3f;

            // Screen Y 값을 조정 (예: 좌우로 이동)
            screenY += moveAmountY;

            // 범위를 벗어나지 않도록 클램핑합니다. (일반적으로 0 ~ 1 사이 값으로 제한합니다)
            screenY = Mathf.Clamp01(screenY);

            // 변경된 값을 적용합니다.
            framingTransposer.m_ScreenY = screenY;
        }
    }

    /// <summary>
    /// 시네머신 카메라를 원하는 타겟으로 옮기는 기능입니다.
    /// </summary>
    /// <param name="Target"></param>
    public void ChangeCameraTarget(Transform Target)
    {
        if (_originalFollowTransform == null)
        {
            _originalFollowTransform = _virtualCamera.Follow;
        }

        _virtualCamera.Follow = Target;
    }

    /// <summary>
    /// ChangeCameraTarget으로 변경했던 타겟을 원래 상태(플레이어)에게 돌리는 기능입니다.
    /// </summary>
    public void ReturnCameraTarget()
    {
        if (_originalFollowTransform != null)
        {
            _virtualCamera.Follow = _originalFollowTransform;
        }
    }
}
