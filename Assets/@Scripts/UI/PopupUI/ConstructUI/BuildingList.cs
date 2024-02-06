using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingList : MonoBehaviour
{
    // Condition => ItemType.Building.item.Stock == 0

    private GameObject _buildingButton;
    private Transform _contentContainer;
    private List<BuildingButton> _buildingButtons;

    public void Initialize()
    {
        _buildingButton = Resources.Load<GameObject>("UI/BuildingButton");
        _contentContainer = transform.Find("Viewport/Content");

        var buildings = ItemManager.Instance.GetItemDict(ItemType.Building);
        foreach(Item building in buildings.Values)
        {
            if(building.Stock == 0)
            {
                var buildingButton = Instantiate(_buildingButton, _contentContainer).GetComponent<BuildingButton>();
                buildingButton.Initialize(); // TODO => required Data Dependency.
                _buildingButtons.Add(buildingButton);
            }
        }


        RefreshValidButtons();
    }

    private void OnEnable()
    {
        if(_buildingButtons == null) return;

        RefreshValidButtons();
    }

    public void RefreshValidButtons()
    {
        foreach(var button in _buildingButtons)
        {
            if(!button.IsValidButton())
            {
                Destroy(button.gameObject);
            }
        }
    }
}
