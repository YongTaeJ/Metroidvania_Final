using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMaterialChanger : MonoBehaviour
{
    public ParticleSystem particleSystem; // 파티클 시스템 참조
    public Material[] materials; // 교체할 머테리얼 배열

    public void ChangeMaterial(string enemy)
    {
        // 적의 이름에 따라 적절한 머테리얼을 찾는 로직 구현
       
        // 여기서는 단순화를 위해 첫 번째 머테리얼을 사용합니다.
        Material selectedMaterial = materials[0]; // 실제 게임에서는 enemyColor에 따라 선택해야 합니다.

        // 파티클 시스템의 렌더러 컴포넌트를 가져옵니다.
        var particleRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        // 머테리얼을 교체합니다.
        particleRenderer.material = selectedMaterial;
    }
}
