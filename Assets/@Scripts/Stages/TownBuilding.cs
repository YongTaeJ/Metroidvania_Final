using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownBuilding : MonoBehaviour
{
    private void Awake()
    {
        CheckBuilding();
    }

    private void CheckBuilding()
    {
        Dictionary<int, Item> buildingItems = ItemManager.Instance.GetItemDict(ItemType.Building);

        foreach (KeyValuePair<int, Item> entry in buildingItems)
        {
            if (entry.Value.Stock > 0)
            {
                var buildingSO = SOManager.Instance.GetBuildingSO(entry.Key);

                GameObject buildingPrefab = buildingSO.BuildingData.buildingPrefab;
                Vector3 buildingPosition = buildingSO.BuildingData.buildingPosition;
                GameObject buildingObject = Instantiate(buildingPrefab, buildingPosition, Quaternion.identity);
            }
        }
    }
}
