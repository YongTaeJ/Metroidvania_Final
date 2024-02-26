using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerBuildingEffect : MonoBehaviour
{
    public void SpawnParticle()
    {
        GameObject buildingParticle = ResourceManager.Instance.InstantiatePrefab("VillagerBuildingParticle", pooling: true);
        buildingParticle.transform.position = new Vector2(transform.position.x, transform.position.y - 3.1f);
    }
}
