using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    #region properties
    public ShoppingList ShoppingList {get; private set;}
    public BuyPopup BuyPopup {get; private set;}
    #endregion

    #region variables
    private TMP_Text _goldText;
    private Button _exitButton;
    #endregion

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

        PopupAction();
        
        var items = ItemManager.Instance.GetItemDict(ItemType.Gold);
        ChangeGoldText(items[0].Stock);
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

    private void PopupAction()
    {
        float time = 1f;

        _exitButton.gameObject.SetActive(false);
        _goldText.gameObject.SetActive(false);

        transform.localScale = Vector2.zero;
        transform.DOScale(1, time).SetEase(Ease.OutBounce);

        TimerManager.Instance.StartTimer(time,
        () =>
        {
            _exitButton.gameObject.SetActive(true);
            _goldText.gameObject.SetActive(true);
        }
        );
    }
}
