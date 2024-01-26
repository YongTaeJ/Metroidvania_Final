using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class TestInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _itemSlot;

    private void Awake()
    {
        var items = ItemManager.Instance.GetItemDict(ItemType.Material);

        Instantiate(_itemSlot, transform).GetComponent<TestInventorySlot>().Initialize(items[0]);
        Instantiate(_itemSlot, transform).GetComponent<TestInventorySlot>().Initialize(items[1]);

        items = ItemManager.Instance.GetItemDict(ItemType.Gold);

        Instantiate(_itemSlot, transform).GetComponent<TestInventorySlot>().Initialize(items[0]);
    }
}
