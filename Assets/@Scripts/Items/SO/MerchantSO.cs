using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMerchant", menuName = "Data/MerchantSO")]
public class MerchantSO : ScriptableObject, IHasID
{
    [SerializeField] private int _ID;
    [SerializeField] private List<Goods> _goodsList;

    public int ID { get {return _ID;} set { } }
    public List<Goods> GoodsList => _goodsList;
}

[Serializable]
public struct Goods
{
    public ItemType ItemType;
    public int ID;
    public int Cost;
}