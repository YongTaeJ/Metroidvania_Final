using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuilding", menuName = "Data/BuildingSO")]
public class BuildingSO : ScriptableObject
{
    // 외부에서 아이템 존재 유무는 ItemManager를 통해 접근 가능
    // 해당 SO는 건축 조건과 강화 요소에 대한 접근
    [SerializeField] private int _ID;
    [SerializeField] private string _constructName;
    [SerializeField] private string _constructDescription;
    [SerializeField] private List<RequiredCondition> _requiredConditions;
    [SerializeField] private List<RequiredMaterial> _requiredMaterials;
    [SerializeField] private List<StatElement> _statElements;

    public int ID => _ID;
    public string ConstructName => _constructName;
    public string ConstructDescription => _constructDescription;
    public List<RequiredCondition> RequiredConditions => _requiredConditions;
    public List<RequiredMaterial> RequiredMaterials => _requiredMaterials;
    public List<StatElement> StatElements => _statElements;

    public bool IsBuildable()
    {
        int curStock;

        // TODO => Foreach 요소들이 초기화되는지 검증 안됨

        foreach(var required in RequiredMaterials)
        {
            curStock = ItemManager.Instance.GetItemStock(ItemType.Material, required.ID);
            if(curStock < required.Stock)
            {
                return false;
            }
        }

        foreach(var required in RequiredConditions)
        {
            if(!ItemManager.Instance.HasItem(required.itemType, required.ID))
                return false;
        }

        return true;
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
    // TODO => StatType
    public float value;
}