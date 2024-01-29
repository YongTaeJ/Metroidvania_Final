using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PopupType
{
    None,
    Status
}

public class UIManager : Singleton<UIManager>
{
    private Transform _fixedUI;
    private Transform _popupUI;
    private Dictionary<PopupType, GameObject> _popupUIElements;

    public override bool Initialize()
    {
        InitCanvases();
        InitPopupElements();

        return base.Initialize();
    }

    private void InitCanvases()
    {
        var fixedUI = new GameObject("@FixedUI");
        var popupUI = new GameObject("@PopupUI");
        InitCanvas(fixedUI);
        InitCanvas(popupUI);
        _fixedUI = fixedUI.transform;
        _popupUI = popupUI.transform;
    }

    private void InitCanvas(GameObject obj)
    {
        var canvas = obj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        var scaler = obj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2 (1920, 1080);

        obj.AddComponent<GraphicRaycaster>();
    }

    private void InitPopupElements()
    {
        _popupUIElements = new Dictionary<PopupType, GameObject>();
        // TODO => 폴더 다 읽은 다음 뒤에 UI 지우고 해당 string으로 enum 매칭시켜서 넣기
        var statusUI = Instantiate(Resources.Load<GameObject>("UI/Status/StatusUI"), _popupUI);
        _popupUIElements.Add(PopupType.Status, statusUI);
        statusUI.SetActive(false);
    }

    public void OnPopupEvent(PopupType popupType)
    {
        _popupUIElements[popupType].SetActive(true);
    }
}
