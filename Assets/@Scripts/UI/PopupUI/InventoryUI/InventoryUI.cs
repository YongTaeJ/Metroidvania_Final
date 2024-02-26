using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryUI : MonoBehaviour
{
    #region properties
    public CategoryButtonContainer CategoryButtonContainer {get; private set;}
    public InventorySlotContanier InventorySlotContanier {get; private set;}
    public Image CategoryImage {get; private set;}
    public ItemPopupUI ItemPopupUI {get; private set;}
    public GameObject ItemPopupUIPrefab {get; private set;}
    #endregion

    #region variables
    private GameObject _dim;
    private GameObject _exitButton;
    private bool _isInitialized = false;
    #endregion

    private void Awake()
    {
        CategoryButtonContainer = transform.Find("DataDependent/ButtonContainer").GetComponent<CategoryButtonContainer>();
        InventorySlotContanier = transform.Find("DataDependent/SlotContainer").GetComponent<InventorySlotContanier>();
        CategoryImage = transform.Find("DataDependent/CategoryImage").GetComponent<Image>();
        ItemPopupUI = transform.Find("Popup/ItemPopupUI").GetComponent<ItemPopupUI>();

        InventorySlotContanier.Initialize(this);
        CategoryButtonContainer.Initialize(this);

        _dim = transform.Find("DataIndependent/Dim").gameObject;

        // ExitButton
        _exitButton = transform.Find("ExitButton").gameObject;
        Button exitButton = _exitButton.GetComponent<Button>();
        exitButton.onClick.AddListener(() => gameObject.SetActive(false));

        // ToBuildingButton
        Button toBuildingUIButton = transform.Find("ToBuildingUIButton").GetComponent<Button>();
        toBuildingUIButton.onClick.AddListener
        ( () =>
        {
            UIManager.Instance.OpenPopupUI(PopupType.Building);
            gameObject.SetActive(false);
        }
        );

        _isInitialized = true;
        
        SetWallet();

        Refresh();
    }

    private void SetWallet()
    {
        Item gold = ItemManager.Instance.GetItem(ItemType.Gold, 0);
        TMP_Text goldText = transform.Find("DataDependent/Wallet/Text").GetComponent<TMP_Text>();
        goldText.text = gold.Stock.ToString() + " Gold";

        gold.OnStockChanged += (x) => goldText.text = x.ToString() + " Gold";
    }

    private void OnEnable()
    {
        if(!_isInitialized) return;

        PopupAction();
        Refresh();
    }

    private void Refresh()
    {
        CategoryImage.gameObject.SetActive(false);
        InventorySlotContanier.gameObject.SetActive(false);
    }

    private void PopupAction()
    {
        float time = 1f;

        _dim.SetActive(false);
        _exitButton.SetActive(false);

        transform.localScale = Vector2.zero;
        transform.DOScale(time, 1).SetEase(Ease.OutBounce);

        TimerManager.Instance.StartTimer(time,
        () =>
        {
            _exitButton.SetActive(true);
            _dim.SetActive(true);
        });
    }
}