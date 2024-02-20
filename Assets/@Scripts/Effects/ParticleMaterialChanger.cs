using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMaterialChanger : MonoBehaviour
{
    public ParticleSystem particleSystem; // 파티클 시스템 참조
    public Material[] materials; // 교체할 머테리얼 배열

    public void ChangeMaterial(string enemy)
    {
        int materialIndex = 0; // 기본 머테리얼 인덱스

        // 적의 이름에 따라 머테리얼 인덱스 결정
        switch (enemy)
        {
            case "Orc_Melee":
            case "Orc_Ranged":
            case "Turret":
                materialIndex = 1; 
                break;
            case "BlueSlime":
            case "Soul":
            case "TutorialSlime": 
                materialIndex = 2; 
                break;
            case "Bat":
                materialIndex = 3; 
                break;
            case "Ground":
                materialIndex = 4;
                break;
            default:
                materialIndex = 0; // 기본값, 명시되지 않은 적의 경우
                break;
        }

        var selectedMaterial = materials[materialIndex];
        var particleRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        particleRenderer.material = selectedMaterial;
    }
}
