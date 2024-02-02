using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class StatusItemContainer : MonoBehaviour
{
    private GameObject _itemSlot;
    private StatusUI _statusUI;
    private List<ItemSlot> _itemSlots;

    public void Initialize(StatusUI statusUI)
    {
        _itemSlots = new List<ItemSlot>();
        _itemSlot = Resources.Load<GameObject>("UI/ItemSlot");
        _statusUI = statusUI;

        InitSkillSlots();
        InitEquipmentSlots();
        InitMaterialSlots();
        InitGoldSlot();
    }

    public void CheckItems()
    {
        foreach(var slot in _itemSlots)
        {
            slot.CheckStock();
        }
    }

    private void InitSkillSlots()
    {
        ItemType type = ItemType.Skill;
        Transform Container = transform.Find(type.ToString());

        var items = ItemManager.Instance.GetItemDict(type);

        foreach (Item item in items.Values)
        {
            ItemSlot slot = Instantiate(_itemSlot, Container).GetComponent<ItemSlot>();
            _itemSlots.Add(slot);
            slot.Initialize(item);
            Button button = slot.GetComponent<Button>();
            button.onClick.AddListener(() => _statusUI.InformContainer.SetItemInform(item));
            slot.gameObject.SetActive(false);
        }
    }

    private void InitEquipmentSlots()
    {
        ItemType type = ItemType.Equipment;
        Transform Container = transform.Find(type.ToString());

        var items = ItemManager.Instance.GetItemDict(type);

        foreach (Item item in items.Values)
        {
            ItemSlot slot = Instantiate(_itemSlot, Container).GetComponent<ItemSlot>();
            _itemSlots.Add(slot);
            slot.Initialize(item);
            Button button = slot.GetComponent<Button>();
            button.onClick.AddListener(() => _statusUI.InformContainer.SetItemInform(item));
            slot.gameObject.SetActive(false);
        }
    }

    private void InitMaterialSlots()
    {
        ItemType type = ItemType.Material;
        Transform Container = transform.Find(type.ToString());

        var items = ItemManager.Instance.GetItemDict(type);

        foreach(Item item in items.Values)
        {
            ItemSlot slot = Instantiate(_itemSlot, Container).GetComponent<ItemSlot>();
            slot.Initialize(item);
            Button button = slot.GetComponent<Button>();
            button.onClick.AddListener(() => _statusUI.InformContainer.SetItemInform(item));
        }
    }

    private void InitGoldSlot()
    {
        ItemType type = ItemType.Gold;
        Transform Container = transform.Find(type.ToString());

        var items = ItemManager.Instance.GetItemDict(type);

        var text = Container.GetComponentInChildren<TextMeshProUGUI>();
        text.text = "소지금 : " + items[0].Stock.ToString() + "Gold";

        // TODO => Delegate를 할당만 하고 -= 연산이 없음;;
        items[0].OnStockChanged += (x) => { text.text = "소지금 : " + x.ToString() + "Gold" ;} ;
    }
}
