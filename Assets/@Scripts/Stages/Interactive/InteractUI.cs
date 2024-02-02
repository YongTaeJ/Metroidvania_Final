using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractUI : Singleton<InteractUI>
{
    private GameObject _interactUI;

    public override bool Initialize()
    {
        _interactUI = UIManager.Instance.GetUI(PopupType.Interact);
        _interactUI.SetActive(false);
        return base.Initialize();
    }
    public void PopUpInteractUI()
    {
        _interactUI.SetActive(true);
    }
    public void CloseInteractUI()
    {
        _interactUI.SetActive(false);
    }
}
