using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructYNPanel : YNPanel
{
    #region variables
    private int _ID;
    private Action OnRefresh;
    #endregion
    
    public void Initialize(int ID)
    {
        _ID = ID;
        base.Initialize();
    }

    public void InitAction(ConstructUI constructUI)
    {
        OnRefresh -= constructUI.BuildingList.RefreshValidButtons;
        OnRefresh += constructUI.BuildingList.RefreshValidButtons;

        OnRefresh -= constructUI.InformPanel.HideInformData;
        OnRefresh += constructUI.InformPanel.HideInformData;
    }

    protected override void OnClickNo()
    {
        Destroy(gameObject);
    }

    protected override void OnClickYes()
    {
        // TODO => 차후순위로 건축 기념 이벤트 혹은 UI를 넣을 수 있을 듯.
        var SO = SOManager.Instance.GetBuildingSO(_ID);

        foreach(var item in SO.RequiredMaterials)
        {
            ItemManager.Instance.UseItem(ItemType.Material, item.ID, item.Stock);
        }

        ItemManager.Instance.AddItem(ItemType.Building, _ID);

        OnRefresh.Invoke();
        
        Destroy(gameObject);
    }
}
