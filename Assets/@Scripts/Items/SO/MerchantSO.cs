using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMerchant", menuName = "Data/MerchantSO")]
public class MerchantSO : ScriptableObject, IHasID
{
    [SerializeField] private int _ID;
    [SerializeField] private List<InternalItemData> _goodsList;

    public int ID { get {return _ID;} set { } }
    public List<InternalItemData> GoodsList => _goodsList;
}