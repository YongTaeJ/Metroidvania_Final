using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuantitySetter : MonoBehaviour
{
    #region variables
    private int _currentWallet;
    private int _currentCost;
    private int _quantity;
    private TMP_Text _quantityText;
    private TMP_Text _costText;
    #endregion

    public void Initialize()
    {
        InitComponents();
    }

    private void InitComponents()
    {
        _quantityText = transform.Find("QuantityText").GetComponent<TMP_Text>();
        _costText = transform.Find("CostText").GetComponent<TMP_Text>();

        Button _minus5Button = transform.Find("-5Button").GetComponent<Button>();
        Button _minus1Button = transform.Find("-1Button").GetComponent<Button>();
        Button _plus5Button = transform.Find("+5Button").GetComponent<Button>();
        Button _plus1Button = transform.Find("+1Button").GetComponent<Button>();

        _minus5Button.onClick.AddListener( () => SetQuantity(-5));
        _minus1Button.onClick.AddListener( () => SetQuantity(-1));
        _plus5Button.onClick.AddListener( () => SetQuantity(+5));
        _plus1Button.onClick.AddListener( () => SetQuantity(+1));
    }

    private void SetQuantity(int value)
    {
        // 1~99개가 최대
        int curValue = _quantity + value;
        int expectCost = curValue * _currentCost;

        if(curValue <= 0)
        {
            _quantity = _currentWallet / _currentCost;
        }
        else if(curValue > 99)
        {
            _quantity = 1;
        }
        else if(expectCost > _currentWallet)
        {
            _quantity = _currentWallet / _currentCost;
        }
        else
        {
            _quantity = curValue;
        }

        _quantityText.text = _quantity.ToString();
        _costText.text = "가격 : " + (_quantity * _currentCost).ToString() + " Gold";
    }

    public int GetCost()
    {
        return _quantity * _currentCost;
    }

    public int GetQuantity()
    {
        return _quantity;
    }

    public void ResetQuantity(int cost)
    {
        _currentWallet = ItemManager.Instance.GetItemStock(ItemType.Gold, 0);
        _currentCost = cost;
        _quantity = 1;

        _quantityText.text = _quantity.ToString();
        _costText.text = "가격 : " + cost.ToString() + " Gold";
    }



}
