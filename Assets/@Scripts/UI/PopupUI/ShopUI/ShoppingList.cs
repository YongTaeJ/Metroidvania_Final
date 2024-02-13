using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    private GameObject _button;
    private Transform _contentContainer;
    private List<GoodsButton> _buttons;

    public void Initialize(BuyPopup popup)
    {
        _button = Resources.Load<GameObject>("UI/GoodsButton");
        _buttons = new List<GoodsButton>();
        _contentContainer = transform.Find("Viewport/Content");        

        for(int i = 0; i < 10; i++)
        {
            var button = Instantiate(_button , _contentContainer).GetComponent<GoodsButton>();
            button.Initialize(popup);
            _buttons.Add(button);
        }
    }

    public void SetMerchantData(int ID)
    {
        var dat = SOManager.Instance.GetMerchantSO(ID);
        int index = 0;

        foreach(var button in _buttons)
        {
            button.gameObject.SetActive(false);
        }

        foreach(var data in dat.GoodsList)
        {
            _buttons[index].SetGoodsData(data);
            index++;
        }
    }
}
