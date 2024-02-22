using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHomeBuildingEffect : MonoBehaviour
{
    public void SpawnEffect()
    {
        GameObject buildingEffect = ResourceManager.Instance.InstantiatePrefab("BuildingEffect", pooling: true);
        float randomX = Random.Range(-3f, 3f);
        float randomY = Random.Range(-2f, 2f);

        Vector3 randomPosition = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);

        buildingEffect.transform.position = randomPosition;
    }

    public void SpawnParticle()
    {
        GameObject buildingParticle = ResourceManager.Instance.InstantiatePrefab("BuildingParticle", pooling: true);
        buildingParticle.transform.position = new Vector2(transform.position.x, transform.position.y - 3.1f);
    }
}
