using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class NPCInteraction : MonoBehaviour
{
    #region variables
    protected ChatBoxUI _chatBoxUI;
    #endregion

    protected virtual void Awake()
    {
        _chatBoxUI = UIManager.Instance.GetUI(PopupType.ChatBox).GetComponent<ChatBoxUI>();
    }

    protected void StartInteract(PlayerInput input)
    {
        input.enabled = false;
        _chatBoxUI.ActiveUI(true);
        UIManager.Instance.ClosePopupUI(PopupType.Interact);
    }

    protected void EndInteract(PlayerInput input)
    {
        _chatBoxUI.ActiveUI(false);
        input.enabled = true;
        UIManager.Instance.OpenPopupUI(PopupType.Interact);
    }

    public abstract IEnumerator Interact(PlayerInput input);
}
