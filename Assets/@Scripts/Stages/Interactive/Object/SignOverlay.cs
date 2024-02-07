using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class SignOverlay : SignBase
{
    protected TextMeshProUGUI _signText;
    [SerializeField]
    protected string _signID;

    private string _signMessage;

    protected override void OpenPopupSign()
    {
        _signMessage = SignDataManager.Instance.GetMessage(_signID);
        _signText = UIManager.Instance.GetUI(PopupType.Chest).GetComponentInChildren<TextMeshProUGUI>();
        _signText.text = _signMessage;
        UIManager.Instance.OpenPopupUI(PopupType.Chest);
    }

    protected override void ClosePopupSign()
    {
        UIManager.Instance.ClosePopupUI(PopupType.Chest);
    }
}