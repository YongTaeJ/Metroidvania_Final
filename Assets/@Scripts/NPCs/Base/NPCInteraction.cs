using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public abstract class NPCInteraction : MonoBehaviour, IInteract
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
        UIManager.Instance.ClosePopupUI(PopupType.Interact);
    }

    protected void EndInteract(PlayerInput input)
    {
        input.enabled = true;
        UIManager.Instance.OpenPopupUI(PopupType.Interact);
    }

    public abstract IEnumerator Interact(PlayerInput input);
}
