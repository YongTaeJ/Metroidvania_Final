using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrcutIndicatorContainer : MonoBehaviour
{
    private List<ConstrcutIndicator> _indicators;
    private GameObject _indicator;

    public void Initialize()
    {
        _indicator = Resources.Load<GameObject>("UI/ConstructIndicator");
        InitIndicators();
    }

    private void InitIndicators()
    {
        _indicators = new List<ConstrcutIndicator>();

        for(int i=0; i < 4 ;i++)
        {
            var indicator = Instantiate(_indicator, transform).GetComponent<ConstrcutIndicator>();
            _indicators.Add(indicator);
            indicator.Initialize();
            indicator.Active(false);
        }
    }

    public void SetIndicators(int buildingID)
    {
        foreach(var indicator in _indicators)
        {
            indicator.Active(false);
        }

        int idx = 0;

        var SO = SOManager.Instance.GetBuildingSO(buildingID);

        foreach(var data in SO.RequiredMaterials)
        {
            Sprite sprite = ItemManager.Instance.GetSprite(ItemType.Material, data.ID);
            int originStock = ItemManager.Instance.GetItemStock(ItemType.Material, data.ID);
            int resultStock = originStock - data.Stock;

            _indicators[idx].SetValue(sprite, originStock, resultStock);
            _indicators[idx].Active(true);

            idx++;
        }
    }


}
