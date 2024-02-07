using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructYNPanel : YNPanel
{
    #region variables
    private int _ID;
    #endregion
    
    public void Initialize(int ID)
    {
        _ID = ID;
        base.Initialize();
    }

    protected override void OnClickNo()
    {
        Destroy(gameObject);
    }

    protected override void OnClickYes()
    {
        // TODO => 차후순위로 건축 기념 이벤트 혹은 UI를 넣을 수 있을 듯.
        var SO = BuildingSOManager.Instance.GetSO(_ID);

        // 유효성 검증 필요 X
        foreach(var item in SO.RequiredMaterials)
        {
            ItemManager.Instance.UseItem(ItemType.Material, item.ID, item.Stock);
        }

        ItemManager.Instance.AddItem(ItemType.Building, _ID);
        
        Destroy(gameObject);
    }
}
