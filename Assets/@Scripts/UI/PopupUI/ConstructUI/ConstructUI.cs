using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ConstructUI : MonoBehaviour
{
    #region properties
    public ConstructInformPanel InformPanel { get; private set; }
    public ConstructList BuildingList { get; private set; }
    public ConstructYNPanel YNPanel {get; private set;}
    #endregion

    #region variables
    private bool _isInitialized = false;
    private GameObject _exitButton;
    private GameObject _dim;
    #endregion

    private void Awake() 
    {
        _dim = transform.Find("DataIndependent/Dim").gameObject;

        // ExitButton
        _exitButton = transform.Find("ExitButton").gameObject;
        Button button = _exitButton.GetComponent<Button>();
        button.onClick.AddListener( () => { gameObject.SetActive(false);});

        InformPanel = GetComponentInChildren<ConstructInformPanel>();
        BuildingList = GetComponentInChildren<ConstructList>();
        YNPanel = GetComponentInChildren<ConstructYNPanel>();

        YNPanel.Initialize(this);
        InformPanel.Initialize(this);
        BuildingList.Initialize(this);

        _isInitialized = true;
    }

    private void OnEnable()
    {
        if(!_isInitialized) return;

        InformPanel.Refresh();

        _exitButton.SetActive(false);
        _dim.SetActive(false);

        transform.position = new Vector3(Screen.width / 2, Screen.height * 2 , 0);
        transform.DOMoveY(Screen.height / 2, 1).SetEase(Ease.OutBack);

        TimerManager.Instance.StartTimer(1f, () =>
        {
            _exitButton.SetActive(true);
            _dim.SetActive(true);
        } ); 
    }
}
