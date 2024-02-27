using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownNPCCheck : MonoBehaviour
{
    private Dictionary<int, GameObject> _NPCs;

    private void Awake()
    {
        _NPCs = new Dictionary<int, GameObject>();

        InitNPCs();
        CheckNPC();
    }

    private void CheckNPC()
    {
        foreach(int ID in _NPCs.Keys)
        {
            bool isValid = ItemManager.Instance.HasItem(ItemType.NPC, ID);
            
            if(isValid)
            {
                _NPCs[ID].SetActive(true);
            }
            else
            {
                _NPCs[ID].SetActive(false);
            }
        }
    }

    private void InitNPCs()
    {
        Transform parentTransform = GameObject.Find("Town/Object/NPC").transform;
        var _NPCPrefabs = Resources.LoadAll<GameObject>("NPCs");

        foreach (GameObject obj in _NPCPrefabs)
        {
            string name = obj.name;
            foreach(var item in ItemManager.Instance.GetItemDict(ItemType.NPC).Values)
            {
                if(item.ItemData.Name == name)
                {
                    _NPCs.Add(item.ItemData.ID, Instantiate(obj, obj.transform.position, obj.transform.rotation, parentTransform));
                    _NPCs[item.ItemData.ID].SetActive(false);
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            CheckNPC();
        }
    }
}
