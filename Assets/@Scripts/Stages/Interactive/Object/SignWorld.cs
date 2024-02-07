using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignWorld : SignBase
{
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponentInChildren<Canvas>(true);
        _canvas.gameObject.SetActive(false);
    }
    protected override void OpenPopupSign()
    {
        _canvas.gameObject.SetActive(true);
    }

    protected override void ClosePopupSign()
    {
        _canvas.gameObject.SetActive(false);
    }
}
