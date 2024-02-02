using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum PopupType
{
    None,
    Status,
    Chest,
    ChatBox,
    Pause,
    Interact
}

public class UIManager : Singleton<UIManager>
{
    #region Variables
    private Transform _fixedUI;
    private Transform _popupUI;
    private Dictionary<PopupType, GameObject> _popupUIElements;
    private List<GameObject> _fixedUIElements;
    #endregion

    #region Initialize
    public override bool Initialize()
    {
        if(base.Initialize())
        {
            InitCanvases();
            InitFixedElements();
            InitPopupElements();
        }

        return true;
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

    private void InitFixedElements()
    {
        _fixedUIElements = new List<GameObject>();
        
        GameObject[] UIs = Resources.LoadAll<GameObject>("UI/FixedUI");

        foreach(var UI in UIs)
        {
            var inGameUI = Instantiate(UI, _fixedUI);
            _fixedUIElements.Add(inGameUI);
            inGameUI.SetActive(true);
        }
    }

    private void InitPopupElements()
    {
        _popupUIElements = new Dictionary<PopupType, GameObject>();
        
        // 프리펩 명명규칙 : EnumType + UI로 제작 ex. StatusUI
        GameObject[] UIs = Resources.LoadAll<GameObject>("UI/PopupUI");

        foreach(var UI in UIs)
        {
            var inGameUI = Instantiate(UI, _popupUI);
            string UIName = UI.name.Substring(0, UI.name.Length - 2);
            if (!Enum.TryParse(UIName, out PopupType type))
            {
                Debug.Log("PopupUI 폴더 확인 필요");
                continue;
            }
            _popupUIElements.Add(type, inGameUI);
            inGameUI.SetActive(false);
        }
    }
    #endregion

    public void PopupUI(PopupType popupType)
    {
        _popupUIElements[popupType].SetActive(true);
    }

    public GameObject GetUI(PopupType popupType)
    {
        return _popupUIElements[popupType];
    }

    public void SetFixedUI(bool isActive)
    {
        foreach(var UI in _fixedUIElements)
        {
            UI.SetActive(isActive);
        }
    }
}
