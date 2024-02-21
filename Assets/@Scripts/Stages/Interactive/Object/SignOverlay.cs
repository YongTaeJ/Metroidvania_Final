using TMPro;
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
        _signText = UIManager.Instance.GetUI(PopupType.ToolTip).GetComponentInChildren<TextMeshProUGUI>();
        _signText.text = _signMessage;
        UIManager.Instance.OpenPopupUI(PopupType.ToolTip);
    }

    protected override void ClosePopupSign()
    {
        UIManager.Instance.ClosePopupUI(PopupType.ToolTip);
    }
}