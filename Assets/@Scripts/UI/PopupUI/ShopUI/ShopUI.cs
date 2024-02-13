using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private ShoppingList _list;
    private BuyPopup _popup;
    private TMP_Text _goldText;
    private Button _exitButton;

    private void Awake()
    {
        _list = transform.Find("DataDependent/ShoppingList").GetComponent<ShoppingList>();
        _popup = transform.Find("DataDependent/BuyPopup").GetComponent<BuyPopup>();
        _goldText = transform.Find("DataDependent/GoldText").GetComponent<TMP_Text>();
        _exitButton = transform.Find("ExitButton").GetComponent<Button>();

        _exitButton.onClick.AddListener(() => gameObject.SetActive(false));

        _popup.gameObject.SetActive(false);

        _popup.Initialize();
        _list.Initialize(_popup);

        ChangeGoldText(ItemManager.Instance.GetItemStock(ItemType.Gold, 0));
    }

    public void OpenUI(int ID)
    {
        gameObject.SetActive(true);
        _list.SetMerchantData(ID);
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
