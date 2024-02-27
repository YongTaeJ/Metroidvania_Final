using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuilding", menuName = "Data/BuildingSO")]
public class BuildingSO : ScriptableObject, IHasID
{
    // 건축 완료 ChatData는 본인 ID + 1000번!
    // 외부에서 아이템 존재 유무는 ItemManager를 통해 접근 가능
    // 해당 SO는 건축 조건과 강화 요소에 대한 접근
    [SerializeField] private int _ID;
    // => 효과는 생산성을 위해 코드를 통한 분류가 아니라 단순 텍스트를 통해 설명
    [SerializeField] private string _effectDescription;
    [SerializeField] private List<RequiredCondition> _requiredConditions;
    [SerializeField] private List<RequiredMaterial> _requiredMaterials;
    [SerializeField] private List<StatElement> _statElements;
    [SerializeField] private List<InternalItemData> _rewardElements;
    [SerializeField] private BuildingData _buildingData;

    public int ID { get {return _ID;} set { } }
    public string EffectDescription => _effectDescription;
    public List<RequiredCondition> RequiredConditions => _requiredConditions;
    public List<RequiredMaterial> RequiredMaterials => _requiredMaterials;
    public List<InternalItemData> RewardsElements => _rewardElements;
    public List<StatElement> StatElements => _statElements;
    public BuildingData BuildingData => _buildingData;

    public bool IsBuildable()
    {
        return MaterialTest() && ConditionTest();
    }

    public bool MaterialTest()
    {
        int curStock;
        foreach(var required in RequiredMaterials)
        {
            curStock = ItemManager.Instance.GetItemStock(ItemType.Material, required.ID);
            if(curStock < required.Stock)
            {
                return false;
            }
        }

        return true;
    }

    public bool ConditionTest()
    {
        foreach(var required in RequiredConditions)
        {
            if(!ItemManager.Instance.HasItem(required.itemType, required.ID))
                return false;
        }

        return true;
    }

    public void GiveReward()
    {
        // 스탯상승, 추가 리워드 제공
        // 둘 중 하나만 있어도 문제없도록
        var curStats = GameManager.Instance.player.playerStatus;

        foreach(var element in StatElements)
        {
            curStats.AddStat(element.statusType, element.value);
        }

        foreach(var element in RewardsElements)
        {
            ItemManager.Instance.AddItem(element.ItemType, element.ID, element.Stock);
        }
    }
}

[Serializable]
public struct RequiredMaterial
{
    public int ID;
    public int Stock;
}

[Serializable]
public struct RequiredCondition
{
    public ItemType itemType;
    public int ID;
}

[Serializable]
public struct StatElement
{
    public PlayerStatusType statusType;
    public float value;
}

[Serializable]
public struct BuildingData
{
    public GameObject buildingPrefab;
    public Vector3 buildingPosition;
}