using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineFramingTransposer framingTransposer;
    private float originScreenY = 0.5f;
    private bool isCameraMove = false; //카메라 움직임과 임펄스가 동시에 일어나면 m_Screen이 이상한 수치에 고정됨
    private Transform _originalFollowTransform;

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
        VirtualCamera = camera;
        framingTransposer = VirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    public void MoveCamera(Vector2 direction)
    {
        isCameraMove = true;

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
        if (framingTransposer != null)
        {
            framingTransposer.m_ScreenY = originScreenY;
        }
        StartCoroutine(ResetCameraMove());
    }

    /// <summary>
    /// 시네머신 카메라를 원하는 타겟으로 옮기는 기능입니다.
    /// </summary>
    /// <param name="Target"></param>
    public void ChangeCameraTarget(Transform Target)
    {
        if (_originalFollowTransform == null)
        {
            _originalFollowTransform = VirtualCamera.Follow;
        }

        VirtualCamera.Follow = Target;
    }

    /// <summary>
    /// ChangeCameraTarget으로 변경했던 타겟을 원래 상태(플레이어)에게 돌리는 기능입니다.
    /// </summary>
    public void ReturnCameraTarget()
    {
        if (_originalFollowTransform != null)
        {
            VirtualCamera.Follow = _originalFollowTransform;
        }
    }


    public void SetCameraTransitionActive(bool isActive)
    {
        if (VirtualCamera != null)
        {
            VirtualCamera.enabled = isActive;
        }
    }
}
