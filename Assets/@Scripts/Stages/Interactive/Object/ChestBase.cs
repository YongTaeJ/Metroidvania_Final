using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestBase : MonoBehaviour
{
    // 이전에 구상했던 것과 다르게
    // 플레이어의 상호작용 키에 연결하여 작동되게 구성
    // 플레이어가 상자를 열 수 있는 범위(콜라이더 이용)안에서 상호작용 키를 누른 경우
    // 상자가 열리며 아이템을 획득할 수 있게
    // 현재 TriggerEnter, Exit으로 구현하였지만,
    // 추후에 TriggerEnter와 Exit은 IsPlayerEnter값만 변경하게 하고
    // 실제 작동은 IsPlayerEnter가 true일때 상호작용키를 누르면 작동하게 변경
    // 상자가 열리는 애니메이션을 넣고, 아이템 등을 획득하고, 몇초 후에 UI가 사라지게

    protected TextMeshProUGUI _chestText;
    protected PlayerInput _playerInput;
    [SerializeField]
    protected int _chestID = 0;
    protected Canvas _press;

    protected virtual void Awake()
    {
        if (ItemManager.Instance.HasItem(ItemType.Chest, _chestID))
        {
            Destroy(gameObject);
        }
        _press = GetComponentInChildren<Canvas>(true);
        if (_press != null) _press.gameObject.SetActive(false);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInput = collision.GetComponent<PlayerInput>();
            var playerInputController = collision.GetComponent<PlayerInputController>();
            playerInputController.OnInteraction += OpenChest;
            if (_press != null) _press.gameObject.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerInputController = collision.GetComponent<PlayerInputController>();
            playerInputController.OnInteraction -= OpenChest;
            if (_press != null) _press.gameObject.SetActive(false);
        }
    }

    protected virtual void OpenChest()
    {
        ChestText();
        ItemManager.Instance.AddItem(ItemType.Chest, _chestID);
        UIManager.Instance.OpenPopupUI(PopupType.AToolTip);
        if (_press != null) _press.gameObject.SetActive(false);

        SFX.Instance.PlayOneShot(ResourceManager.Instance.GetAudioClip("Chest"));

        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("IsOpen", true);
        }

        Collider2D collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;

        StartCoroutine(CoChestTextOff());
    }

    private IEnumerator CoChestTextOff()
    {
        yield return new WaitForSeconds(1.2f);
        UIManager.Instance.ClosePopupUI(PopupType.AToolTip);
        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = false;
        yield return new WaitForSeconds(1.5f);
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void ChestText()
    {
        _chestText = UIManager.Instance.GetUI(PopupType.AToolTip).GetComponentInChildren<TextMeshProUGUI>();
        _chestText.text = "The chest is empty";
    }
}
