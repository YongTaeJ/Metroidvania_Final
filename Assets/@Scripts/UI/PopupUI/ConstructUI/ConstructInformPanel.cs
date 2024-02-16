using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstructInformPanel : MonoBehaviour, IInformable
{
    #region variables
    private Image _buildingImage;
    private TMP_Text _nameText;
    private TMP_Text _conditionText;
    private TMP_Text _descriptionText;
    private TMP_Text _materialText;
    private ConstructButton _constructButton;
    #endregion
    
    public void Initialize(ConstructUI constructUI)
    {
        InitComponents();
        _constructButton.Initialize(constructUI);
    }

    private void InitComponents()
    {
        _buildingImage = transform.Find("BuildingImage").GetComponent<Image>();
        _nameText = transform.Find("NameText").GetComponent<TMP_Text>();
        _conditionText = transform.Find("ConditionText").GetComponent<TMP_Text>();
        _descriptionText = transform.Find("DescriptionText").GetComponent<TMP_Text>();
        _materialText = transform.Find("MaterialText").GetComponent<TMP_Text>();
        _constructButton = transform.Find("ConstructButton").GetComponent<ConstructButton>();
    }

    public void SetInformPanel(Item item)
    {
        int ID = item.ItemData.ID;

        var data = ItemManager.Instance.GetItemData(ItemType.Building, ID);
        var sprite = ItemManager.Instance.GetSprite(ItemType.Building, ID);
        var SO = SOManager.Instance.GetBuildingSO(ID);

        _buildingImage.sprite = sprite;
        _nameText.text = data.Name;
        SetConditionText(SO.RequiredConditions);
        _descriptionText.text = SO.ConstructDescription;
        SetMaterialText(SO.RequiredMaterials);

        if(SO.IsBuildable())
        {
            _constructButton.Enable(ID);
        }
        else
        {
            _constructButton.Disable();
        }
    }

    private void SetConditionText(List<RequiredCondition> conditions)
    {
        if(conditions.Count == 0)
        {
            _conditionText.alignment = TextAlignmentOptions.Center;
            _conditionText.text = "없음";
            return;
        }

        StringBuilder content = new StringBuilder();
        content.Append("건설 요구사항: ");

        foreach(var condition in conditions)
        {
            content.Append(ItemManager.Instance.GetItemData(condition.itemType, condition.ID).Name);
            content.Append(", ");
        }

        content.Remove(content.Length-2, 2);
        content.Append(" 보유");

        _conditionText.alignment = TextAlignmentOptions.CaplineLeft;
        _conditionText.text = content.ToString();
    }

    private void SetMaterialText(List<RequiredMaterial> materials)
    {
        if(materials.Count == 0)
        {
            Debug.Log("건설 시 요구 재료는 꼭 필요합니다. 추가 요망!!");
            _materialText.text = "";
            return;
        }

        StringBuilder content = new StringBuilder();
        content.Append("재료 : "); 

        foreach(var material in materials)
        {
            content.Append(ItemManager.Instance.GetItemData(ItemType.Material, material.ID).Name);
            content.Append(' ');
            content.Append(material.Stock);
            content.Append("개, ");
        }

        content.Remove(content.Length-2, 2);

        _materialText.alignment = TextAlignmentOptions.CaplineLeft;
        _materialText.text = content.ToString();
    }
}
