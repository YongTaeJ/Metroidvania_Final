using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlots : MonoBehaviour
{
    private InventoryItemSlot[] _itemSlots;
    public void Initialize(InventoryUI UI, ItemType type)
    {
        _itemSlots = new InventoryItemSlot[12];

        GameObject ItemSlotPrefab = Resources.Load<GameObject>("UI/InventoryItemSlot");
        var items = ItemManager.Instance.GetItemDict(type);

        for(int i=0; i<12; i++)
        {
            var itemSlot = Instantiate(ItemSlotPrefab, transform).GetComponent<InventoryItemSlot>();
            _itemSlots[i] = itemSlot;
            if (items.ContainsKey(i))
            {
                _itemSlots[i].Initialize(type, i);
                Button button = _itemSlots[i].GetComponent<Button>();
                var item = items[i];
                button.onClick.AddListener(
                () =>
                {
                    UI.ItemPopupUI.SetInform(item);
                });
            }
        }
    }

    public void ActiveSlots(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
