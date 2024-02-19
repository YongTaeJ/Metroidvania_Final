using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotContanier : MonoBehaviour
{
    private Dictionary<ItemType, InventoryItemSlots> _inventorySlots;

    public void Initialize(InventoryUI UI)
    {
        _inventorySlots = new Dictionary<ItemType, InventoryItemSlots>();

        var materialSlots = transform.Find("MaterialSlots").GetComponent<InventoryItemSlots>();
        var skillSlots = transform.Find("SkillSlots").GetComponent<InventoryItemSlots>();
        var equipmentSlots = transform.Find("EquipmentSlots").GetComponent<InventoryItemSlots>();

        materialSlots.Initialize(UI, ItemType.Material);
        skillSlots.Initialize(UI, ItemType.Skill);
        equipmentSlots.Initialize(UI, ItemType.Equipment);

        _inventorySlots[ItemType.Material] = materialSlots;
        _inventorySlots[ItemType.Skill] = skillSlots;
        _inventorySlots[ItemType.Equipment] = equipmentSlots;

        Refresh();
    }

    public void ShowSlots(ItemType type)
    {
        Refresh();
        InventoryItemSlots currentSlots = _inventorySlots[type];
        currentSlots.gameObject.SetActive(true);
    }

    public void Refresh()
    {
        foreach(InventoryItemSlots slots in _inventorySlots.Values)
        {
            slots.gameObject.SetActive(false);
        }
    }
}
