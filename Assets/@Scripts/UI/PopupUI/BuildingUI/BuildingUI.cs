using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    #region properties
    public BuildingInformPanel BuildingInformPanel {get; private set;}
    public BuildingList BuildingList {get; private set;}
    #endregion

    #region variables
    public GameObject _exitButton;
    private bool _isInitialized = false;
    #endregion

    private void Awake()
    {
        BuildingInformPanel = transform.Find("DataDependent/InformPanel").GetComponent<BuildingInformPanel>();
        BuildingList = transform.Find("DataDependent/BuildingList").GetComponent<BuildingList>();

        BuildingList.Initialize(this);

        // ExitButton
        _exitButton = transform.Find("ExitButton").gameObject;
        Button exitButton = _exitButton.GetComponent<Button>();
        exitButton.onClick.AddListener(() => gameObject.SetActive(false) );

        // ToBuildingButton
        Button toInventoryUIButton = transform.Find("ToInventoryUIButton").GetComponent<Button>();
        toInventoryUIButton.onClick.AddListener
        ( () =>
        {
            UIManager.Instance.OpenPopupUI(PopupType.Inventory);
            gameObject.SetActive(false);
        }
        );

        _isInitialized = true;
    }

    private void OnEnable()
    {
        if(!_isInitialized) return;

        _exitButton.SetActive(false);
        transform.position = new Vector3(960, 2160, 0);
        transform.DOMoveY(540, 1).SetEase(Ease.OutBack);
        TimerManager.Instance.StartTimer(1f, () => _exitButton.SetActive(true));
    }
}
