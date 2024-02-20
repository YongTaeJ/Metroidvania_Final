using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    #region properties
    public CategoryButtonContainer CategoryButtonContainer {get; private set;}
    public InventorySlotContanier InventorySlotContanier {get; private set;}
    public Image CategoryImage {get; private set;}
    public ItemPopupUI ItemPopupUI {get; private set;}
    public GameObject ItemPopupUIPrefab {get; private set;}
    #endregion

    private bool _isInitialized = false;

    private void Awake()
    {
        CategoryButtonContainer = transform.Find("DataDependent/ButtonContainer").GetComponent<CategoryButtonContainer>();
        InventorySlotContanier = transform.Find("DataDependent/SlotContainer").GetComponent<InventorySlotContanier>();
        CategoryImage = transform.Find("DataDependent/CategoryImage").GetComponent<Image>();
        ItemPopupUI = transform.Find("Popup/ItemPopupUI").GetComponent<ItemPopupUI>();

        InventorySlotContanier.Initialize(this);
        CategoryButtonContainer.Initialize(this);

        // ExitButton
        Button exitButton = transform.Find("ExitButton").GetComponent<Button>();
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
        Refresh();
    }

    private void Refresh()
    {
        CategoryImage.gameObject.SetActive(false);
    }
}