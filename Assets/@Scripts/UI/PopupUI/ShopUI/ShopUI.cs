using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public ShoppingList ShoppingList {get; private set;}
    public BuyPopup BuyPopup {get; private set;}

    private TMP_Text _goldText;
    private Button _exitButton;

    private void Awake()
    {
        ShoppingList = transform.Find("DataDependent/ShoppingList").GetComponent<ShoppingList>();
        BuyPopup = transform.Find("DataDependent/BuyPopup").GetComponent<BuyPopup>();
        _goldText = transform.Find("DataDependent/GoldText").GetComponent<TMP_Text>();
        _exitButton = transform.Find("ExitButton").GetComponent<Button>();

        _exitButton.onClick.AddListener(() => gameObject.SetActive(false));

        BuyPopup.gameObject.SetActive(false);

        BuyPopup.Initialize(this);
        ShoppingList.Initialize(this);

        ChangeGoldText(ItemManager.Instance.GetItemStock(ItemType.Gold, 0));
    }

    public void OpenUI(int ID)
    {
        gameObject.SetActive(true);
        ShoppingList.SetMerchantData(ID);
    }

    private void OnEnable()
    {
        if(_goldText == null) return;
        
        var items = ItemManager.Instance.GetItemDict(ItemType.Gold);
        items[0].OnStockChanged -= ChangeGoldText;
        items[0].OnStockChanged += ChangeGoldText;
    }

    private void OnDisable()
    {
        var items = ItemManager.Instance.GetItemDict(ItemType.Gold);
        items[0].OnStockChanged -= ChangeGoldText;
    }

    private void ChangeGoldText(int x)
    {
        _goldText.text = "소지금 : " + x.ToString() + "Gold";
    }
}
