using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ConstructList와 거의 같은 구조
/// </summary>
public class BuildingList : MonoBehaviour
{
    private List<BuildingButton> _buildingButtons;
    private GameObject _buildingButtonPrefab;
    private Transform _contentContainer;
    private bool _isInitialized = false;

    public void Initialize(BuildingUI parents)
    {
        _buildingButtons = new List<BuildingButton>();
        _buildingButtonPrefab = Resources.Load<GameObject>("UI/BuildingButton");
        _contentContainer = transform.Find("Viewport/Content");

        var buildings = ItemManager.Instance.GetItemDict(ItemType.Building);
        foreach(Item building in buildings.Values)
        {
            var buildingButton = Instantiate(_buildingButtonPrefab, _contentContainer).GetComponent<BuildingButton>();
            buildingButton.Initialize(building.ItemData.ID);
            buildingButton.InitAction(parents);
            _buildingButtons.Add(buildingButton);
        }
        _isInitialized = true;
    }

    private void OnEnable()
    {
        if(!_isInitialized) return;

        foreach(var button in _buildingButtons)
        {
            button.RefreshButton();
        }
    }
}
