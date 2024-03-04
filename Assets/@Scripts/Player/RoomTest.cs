using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTest : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineConfiner confiner;
    private StageChangeUI _stageChangeUI;
    private StageImageUI _stageImageUI;
    private Vector2 _movePos;
    private string lastCollisionTag = "";
    private bool isTransitioning = false;

    private void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner>();
        _stageChangeUI = UIManager.Instance.GetUI(PopupType.StageChange).GetComponent<StageChangeUI>();
        _stageImageUI = UIManager.Instance.GetUI(PopupType.StageImege).GetComponent<StageImageUI>();
        CameraManager.Instance.GetCamera(_virtualCamera);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTransitioning) return;

        if ((collision.CompareTag("Tutorial") || collision.CompareTag("Town") || collision.CompareTag("Stage01") || collision.CompareTag("Stage02")) && confiner != null)
        {
            isTransitioning = true;
            // 맵 이동 후 아직 안꺼졌는데 다시 이동할때 끄기
            if (_stageImageUI.gameObject.activeSelf)
            {
                UIManager.Instance.ClosePopupUI(PopupType.StageImege);
            }
            // 같은 태그의 경우에는 카메라 컨피너 설정만 업데이트
            if (lastCollisionTag == collision.tag)
            {
                confiner.m_BoundingShape2D = collision;
                confiner.InvalidatePathCache();
                isTransitioning = false;
            }
            else
            {
                if(GameManager.Instance.player.Init)
                {
                    lastCollisionTag = collision.tag;
                    DetermineCollisionDirection(collision);

                    // 플레이어 입력 비활성화 후 약간의 이동
                    StartCoroutine(PlayerMovePos());
                    GameManager.Instance.player._invincibilityTime = 3f;
                    GameManager.Instance.player.Invincible = true;

                    // 팝업 UI 표시
                    UIManager.Instance.OpenPopupUI(PopupType.StageChange);
                    UIManager.Instance.SetFixedUI(false);

                    // 페이드 아웃 시작
                    _stageChangeUI.FadeOut();

                    // 카메라 전환 코루틴 시작
                    StartCoroutine(CameraTransition(collision));
                }
                else //처음 시작시 Fade out 진행되지 않고 검은 화면에서 바로 Fade In
                {
                    lastCollisionTag = collision.tag;
                    DetermineCollisionDirection(collision);

                    StartCoroutine(PlayerMovePos());
                    GameManager.Instance.player._invincibilityTime = 3f;
                    GameManager.Instance.player.Invincible = true;

                    UIManager.Instance.OpenPopupUI(PopupType.StageChange);
                    UIManager.Instance.SetFixedUI(false);

                    _stageChangeUI.SetDarkScreen();

                    StartCoroutine(CameraTransition(collision));
                }
            }
        }
    }

    private IEnumerator CameraTransition(Collider2D collision)
    {
        yield return new WaitForSeconds(_stageChangeUI.fadeDuration);
        BGM.Instance.Stop();
        confiner.m_BoundingShape2D = collision;
        confiner.InvalidatePathCache();
        isTransitioning = false;
        string stageText = GetStageText(collision.tag);
        string stageBGM = GetStageBGM(collision.tag);

        _stageChangeUI.FadeIn(() => {
            UIManager.Instance.SetFixedUI(true);
            UIManager.Instance.ClosePopupUI(PopupType.StageChange);
            UIManager.Instance.OpenPopupUI(PopupType.StageImege);
            _stageImageUI.StartStageUI(stageText);
            GameManager.Instance.player._playerInput.enabled = true;
            GameManager.Instance.player.Init = true;
            BGM.Instance.Play(stageBGM, true);
        });
    }

    private void DetermineCollisionDirection(Collider2D collision)
    {
        Vector2 direction = collision.transform.position - transform.position;
        direction.Normalize();

        _movePos = direction.x > 0 ? Vector2.right : Vector2.left;
    }

    private IEnumerator PlayerMovePos()
    {
        GameManager.Instance.player._playerInput.enabled = false;
        GameManager.Instance.player._controller.Move(_movePos);
        yield return new WaitForSeconds(0.3f);
        GameManager.Instance.player._controller.Move(Vector2.zero);
        GameManager.Instance.player._controller.CanMove = true;
    }

    private string GetStageText(string tag)
    {
        switch (tag)
        {
            case "Tutorial":
                return "시작의 숲";
            case "Town":
                return "마을";
            case "Stage01":
                return "숲";
            case "Stage02":
                return "대성당";
            default:
                return "알 수 없는 지역"; // 기본값
        }
    }

    private string GetStageBGM(string tag)
    {
        switch (tag)
        {
            case "Tutorial":
                return "Town";
            case "Town":
                return "Town";
            case "Stage01":
                return "Stage01";
            case "Stage02":
                return "Stage02";
            default:
                return null;
        }
    }
}
