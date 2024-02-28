using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMap : ChestBase
{
    [SerializeField] private int _mapDataLimit;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }

    protected override void OpenChest()
    {
        int _curMapNumber;

        base.OpenChest();

        Animator animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetBool("IsOpen", true);
        }
        SFX.Instance.PlayOneShot(ResourceManager.Instance.GetAudioClip("ChestMap"));
        _curMapNumber = ItemManager.Instance.GetItemStock(ItemType.Map, 0);

        for (int i = _curMapNumber; i <_mapDataLimit; i++)
        {
            
            ItemManager.Instance.AddItem(ItemType.Map, 0);
        }
    }

    protected override void ChestText()
    {
        base.ChestText();
        _chestText.text = "지도 조각을 얻었다";
        UIManager.Instance.OpenPopupUI(PopupType.AToolTip);
    }
}
