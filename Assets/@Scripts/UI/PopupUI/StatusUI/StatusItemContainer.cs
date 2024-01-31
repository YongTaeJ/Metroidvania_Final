using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusItemContainer : MonoBehaviour
{
    private GameObject _itemSlot;
    private StatusUI _statusUI;

    public void Initialize(StatusUI statusUI)
    {
        _itemSlot = Resources.Load<GameObject>("UI/ItemSlot");
        _statusUI = statusUI;

        InitSkillSlots();
        InitEquipmentSlots();
        InitMaterialSlots();
        InitGoldSlot();
    }

    private void InitSkillSlots()
    {
        // TODO => 스킬 아이템 구상 끝나면
    }

    private void InitEquipmentSlots()
    {
        // TODO => 기믹템 구상 끝나면
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
            button.onClick.AddListener(() => _statusUI.InformContainer.SetItemInform(item) );
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
