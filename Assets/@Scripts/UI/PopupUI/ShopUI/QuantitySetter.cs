using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuantitySetter : MonoBehaviour
{
    #region variables
    private int _quantity;
    private TMP_Text _quantityText;
    #endregion

    public void Initialize()
    {
        InitComponents();
        ResetQuantity();
    }

    private void InitComponents()
    {
        _quantityText = transform.Find("QuantityText").GetComponent<TMP_Text>();

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
        int curValue = _quantity + value;

        if(curValue <= 0)
        {
            _quantity = 99;
        }
        else if(curValue > 99)
        {
            _quantity = 0;
        }
        else
        {
            _quantity = curValue;
        }

        _quantityText.text = _quantity.ToString();
    }

    public int GetQuantity()
    {
        return int.Parse(_quantityText.text);
    }

    public void ResetQuantity()
    {
        _quantity = 1;
        _quantityText.text = _quantity.ToString();
    }



}
