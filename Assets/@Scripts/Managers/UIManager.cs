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
    ToolTip,
    ChatBox,
    Pause,
    Interact,
    GameOver,
    Construct,
    Shop,
    Inventory,
    Building
}

public enum DisposableType
{
    None,
}

public class UIManager : Singleton<UIManager>
{
    #region Variables
    private Transform _fixedUI;
    private Transform _popupUI;
    private Transform _disposableUI;
    public Transform TempUI { get; private set; }
    private Dictionary<PopupType, GameObject> _popupUIElements;
    private Dictionary<DisposableType, GameObject> _disposableUIElements;
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
            InitDisposableElements();
        }
        return true;
    }

    private void InitCanvases()
    {
        var fixedUI = new GameObject("@FixedUI");
        var popupUI = new GameObject("@PopupUI");
        var disposableUI = new GameObject("@DisposableUI");
        var tempUI = new GameObject("@TemporaryUI");

        InitCanvas(fixedUI);
        InitCanvas(popupUI);
        InitCanvas(disposableUI);
        InitCanvas(tempUI);

        _fixedUI = fixedUI.transform;
        _popupUI = popupUI.transform;
        _disposableUI = disposableUI.transform;
        TempUI = tempUI.transform;

        // Temp Code.
        tempUI.GetComponent<Canvas>().sortingOrder = 2;
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

    private void InitDisposableElements()
    {
        _disposableUIElements = new Dictionary<DisposableType, GameObject>();
        
        // 프리펩 명명규칙 : EnumType + UI로 제작 ex. StatusUI
        GameObject[] UIs = Resources.LoadAll<GameObject>("UI/DisposableUI");

        foreach(var UI in UIs)
        {
            string UIName = UI.name.Substring(0, UI.name.Length - 2);
            if (!Enum.TryParse(UIName, out DisposableType type))
            {
                Debug.Log("DisposableUI 폴더 확인 필요");
                continue;
            }
            _disposableUIElements.Add(type, UI);
        }
    }
    #endregion

    public void OpenPopupUI(PopupType popupType)
    {
        _popupUIElements[popupType].SetActive(true);
    }

    public void ClosePopupUI(PopupType popupType)
    {
        if(_popupUIElements[popupType] == null) return;
        
        _popupUIElements[popupType].SetActive(false);
    }

    public void MakeDisposableUI(DisposableType disposableType)
    {
        Instantiate(_disposableUIElements[disposableType], _disposableUI);
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

    public void OpenShopUI(int ID)
    {
        GetUI(PopupType.Shop).GetComponent<ShopUI>().OpenUI(ID);
    }
}
