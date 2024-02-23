using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIManager.Instance.OpenPopupUI(PopupType.StageChange);
            StartCoroutine(PlayerMove());
        }
    }

    private IEnumerator PlayerMove()
    {
        CameraManager.Instance.SetCameraTransitionActive(false);
        GameManager.Instance.player._playerInput.enabled = false;
        GameManager.Instance.player._controller.Move(Vector2.right);
        yield return new WaitForSeconds(0.3f);
        GameManager.Instance.player._controller.Move(Vector2.zero);
        yield return new WaitForSeconds(1f);
        CameraManager.Instance.SetCameraTransitionActive(true);
        UIManager.Instance.GetUI(PopupType.StageChange).GetComponent<StageChangeUI>().StartFadeOut();
        yield return new WaitForSeconds(1f);
        GameManager.Instance.player._playerInput.enabled = true;
    }
}
