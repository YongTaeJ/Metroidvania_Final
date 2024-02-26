using System.Collections;
using TMPro;
using Unity.VisualScripting;
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
        _signText = UIManager.Instance.GetUI(PopupType.AToolTip).GetComponentInChildren<TextMeshProUGUI>();
        _signText.text = _signMessage;
        UIManager.Instance.OpenPopupUI(PopupType.AToolTip);
    }

    protected override void ClosePopupSign()
    {
        StartCoroutine(CoClosePopupSign());
    }

    private IEnumerator CoClosePopupSign()
    {
        yield return new WaitForSeconds(0.7f);
        UIManager.Instance.ClosePopupUI(PopupType.AToolTip);
    }
}