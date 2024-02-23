using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public abstract class NPCInteraction : MonoBehaviour
{
    #region variables
    protected ChatBoxUI _chatBoxUI;
    protected Canvas _press;
    #endregion

    protected virtual void Awake()
    {
        _chatBoxUI = UIManager.Instance.GetUI(PopupType.ChatBox).GetComponent<ChatBoxUI>();
        _press = GetComponentInChildren<Canvas>(true);
        if (_press != null) _press.gameObject.SetActive(false);
    }

    protected void StartInteract(PlayerInput input)
    {
        input.enabled = false;
        if (_press != null) _press.gameObject.SetActive(false);
    }

    protected void EndInteract(PlayerInput input)
    {
        input.enabled = true;
        if (_press != null) _press.gameObject.SetActive(true);
    }

    public abstract IEnumerator Interact(PlayerInput input);
}
