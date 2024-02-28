using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class YNPanel : MonoBehaviour
{
    #region variables
    private Button _yesButton;
    private Button _noButton;
    #endregion

    protected virtual void Initialize()
    {
        _yesButton = transform.Find("YesButton").GetComponent<Button>();
        _noButton = transform.Find("NoButton").GetComponent<Button>();

        _yesButton.onClick.AddListener(OnClickYes);
        _noButton.onClick.AddListener(OnClickNo);
    }

    protected abstract void OnClickYes();
    protected abstract void OnClickNo();
}
