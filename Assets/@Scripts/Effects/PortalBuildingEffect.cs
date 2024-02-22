using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBuildingEffect : MonoBehaviour
{
    public void SpawnParticle()
    {
        GameObject buildingParticle = ResourceManager.Instance.InstantiatePrefab("PortalBuildingParticle", pooling: true);
        buildingParticle.transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
    }
}
