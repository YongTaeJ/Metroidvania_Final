using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButtonContainer : MonoBehaviour
{
    private InventorySlotContanier _inventorySlotContainer;
    private Image _categoryImage;

    public void Initialize(InventoryUI UI)
    {
        _inventorySlotContainer = UI.InventorySlotContanier;
        _categoryImage = UI.CategoryImage;

        Button materialButton = transform.Find("MaterialButton").GetComponent<Button>();
        Button skillButton = transform.Find("SkillButton").GetComponent<Button>();
        Button equipmentButton = transform.Find("EquipmentButton").GetComponent<Button>();

        materialButton.onClick.AddListener(() => OnClickCategory(ItemType.Material));
        skillButton.onClick.AddListener(() => OnClickCategory(ItemType.Skill));
        equipmentButton.onClick.AddListener(() => OnClickCategory(ItemType.Equipment));
    }

    private void OnClickCategory(ItemType type)
    {
        _categoryImage.gameObject.SetActive(true);
        switch(type)
        {
            case ItemType.Material : _categoryImage.sprite = ItemManager.Instance.GetSprite("Material"); break;
            case ItemType.Skill : _categoryImage.sprite = ItemManager.Instance.GetSprite("SkillBook"); break;
            case ItemType.Equipment : _categoryImage.sprite = ItemManager.Instance.GetSprite("Equipment"); break;
        }
        _inventorySlotContainer.ShowSlots(type);
    }
}
