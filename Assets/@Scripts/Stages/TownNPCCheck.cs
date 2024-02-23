using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownNPCCheck : MonoBehaviour
{
    private GameObject[] _NPCPrefabs;

    private void Awake()
    {
        Transform parentTransform = GameObject.Find("Town/Object/NPC").transform;
        _NPCPrefabs = Resources.LoadAll<GameObject>("NPCs");

        foreach (GameObject obj in _NPCPrefabs)
        {
            GameObject npcInstance = Instantiate(obj, obj.transform.position, obj.transform.rotation, parentTransform);
            npcInstance.SetActive(false);
        }
        CheckNPC();
    }

    private void CheckNPC()
    {
        foreach (GameObject obj in _NPCPrefabs)
        {
            //if(ItemManager.Instance.HasItem(ItemType.NPC, ))
            //{
            //    obj.SetActive(true);
            //}
        }
    }
}
