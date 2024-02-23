using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DungeonEnter : MonoBehaviour
{
    protected PlayerInput _playerInput;
    protected Canvas _press;

    private void Awake()
    {
        _press = GetComponentInChildren<Canvas>(true);
        if (_press != null) _press.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInput = collision.GetComponent<PlayerInput>();
            var playerInputController = collision.GetComponent<PlayerInputController>();
            playerInputController.OnInteraction += EnterDungeon;
            if (_press != null) _press.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInput = collision.GetComponent<PlayerInput>();
            var playerInputController = collision.GetComponent<PlayerInputController>();
            playerInputController.OnInteraction -= EnterDungeon;
            if (_press != null) _press.gameObject.SetActive(false);
        }
    }

    private void EnterDungeon()
    {
        EnterDungeonText();
        GameManager.Instance.player.transform.position = new Vector3(290, -100, 0);
        BGM.Instance.Stop();
        BGM.Instance.Play("Stage1", true);
        StartCoroutine(CoChestTextOff());
    }
    private IEnumerator CoChestTextOff()
    {
        MapManager.Instance.LoadImage(true);
        yield return new WaitForSeconds(1f);
        MapManager.Instance.LoadImage(false);
        UIManager.Instance.OpenPopupUI(PopupType.ToolTip);
        yield return new WaitForSeconds(1.8f);
        UIManager.Instance.ClosePopupUI(PopupType.ToolTip);
    }

    private void EnterDungeonText()
    {
        TextMeshProUGUI Text = UIManager.Instance.GetUI(PopupType.ToolTip).GetComponentInChildren<TextMeshProUGUI>();
        Text.text = "You Enter The Dungeon";
    }
}
