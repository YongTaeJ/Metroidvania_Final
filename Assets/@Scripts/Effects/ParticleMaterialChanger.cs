using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMaterialChanger : MonoBehaviour
{
    public ParticleSystem particleSystem; // 파티클 시스템 참조
    public Material[] materials; // 교체할 머테리얼 배열

    public void ChangeMaterial(string tag)
    {
        int materialIndex;

        // 적의 이름에 따라 머테리얼 인덱스 결정
        switch (tag)
        {
            case "Green":
                materialIndex = 1;
                break;
            case "Blue":
                materialIndex = 2;
                break;
            case "Purple":
                materialIndex = 3;
                break;
            case "Wall":
                materialIndex = 4;
                break;
            default:
                materialIndex = 0; // 기본값, 명시되지 않은 태그의 경우
                break;
        }

        var selectedMaterial = materials[materialIndex];
        var particleRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        particleRenderer.material = selectedMaterial;
    }
}
