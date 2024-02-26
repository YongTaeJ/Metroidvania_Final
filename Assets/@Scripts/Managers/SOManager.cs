using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class SOManager : Singleton<SOManager>
{
    private Dictionary<int, BuildingSO> _buildingSODict = new Dictionary<int, BuildingSO>();
    private Dictionary<int, MerchantSO> _merchantSODict = new Dictionary<int, MerchantSO>();
    private Dictionary<int, QuestSO> _questSODict = new Dictionary<int, QuestSO>();
    private const string DefaultPath = "Items/SO/";

    public override bool Initialize()
    {
        if(base.Initialize())
        {
            InitSODatas("Buildings", _buildingSODict);
            InitSODatas("Merchants", _merchantSODict);
            InitSODatas("Quests", _questSODict);
        }
        return true;
    }

    private void InitSODatas<T>(string leftPath, Dictionary<int, T> target) where T : ScriptableObject, IHasID
    {
        string SOPath = DefaultPath + leftPath;

        var SOArr = Resources.LoadAll<T>(SOPath);
        foreach(var SO in SOArr)
        {
            target.Add(SO.ID, SO);
        }
    }

    public BuildingSO GetBuildingSO(int ID)
    {
        return _buildingSODict[ID];
    }

    public MerchantSO GetMerchantSO(int ID)
    {
        return _merchantSODict[ID];
    }

    public QuestSO GetQuestSO(int ID)
    {
        return _questSODict[ID];
    }
}
