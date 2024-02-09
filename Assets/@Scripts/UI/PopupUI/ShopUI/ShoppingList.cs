using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    private GameObject _button;
    private Transform _contentContainer;
    private List<GoodsButton> _buttons;

    public void Initialize()
    {
        _button = Resources.Load<GameObject>("UI/GoodsButton");
        _buttons = new List<GoodsButton>();
        _contentContainer = transform.Find("Viewport/Content");

        // TODO => 상점 리스트에 맞는 아이템 생성
    }
}
