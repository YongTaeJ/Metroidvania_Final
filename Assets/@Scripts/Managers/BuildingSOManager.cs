using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class BuildingSOManager : Singleton<BuildingSOManager>
{
    private Dictionary<int,BuildingSO> _SODict;

    public override bool Initialize()
    {
        if(base.Initialize())
        {
            InitSODatas();
        }
        return true;
    }

    private void InitSODatas()
    {
        _SODict = new Dictionary<int, BuildingSO>();
        var SOArr = Resources.LoadAll<BuildingSO>("Buildings/SO");
        foreach(var SO in SOArr)
        {
            _SODict.Add(SO.ID, SO);
        }
    }

    public BuildingSO GetSO(int ID)
    {
        return _SODict[ID];
    }
}
