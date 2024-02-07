using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class SignOverlay : SignBase
{
    [SerializeField]
    protected TextMeshProUGUI _signText;
    [SerializeField]
    protected string _signID;

    private string _signMessage;

    private void Awake()
    {

    }


    protected override void OpenPopupSign()
    {
        _signMessage = SignDataManager.Instance.GetMessage(_signID);
        _signText = UIManager.Instance.GetUI(PopupType.Sign).GetComponentInChildren<TextMeshProUGUI>();
        _signText.text = _signMessage;
        UIManager.Instance.OpenPopupUI(PopupType.Sign);
    }

    protected override void ClosePopupSign()
    {
        UIManager.Instance.ClosePopupUI(PopupType.Sign);
    }
}