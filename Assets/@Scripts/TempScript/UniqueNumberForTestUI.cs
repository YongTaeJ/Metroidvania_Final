using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UniqueNumberForTestUI : MonoBehaviour
{
    private TMP_Text _uniqueNumberText;
    private void Awake()
    {
        _uniqueNumberText = transform.Find("UniqueNumber").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        // TODO => 플레이어 생성할 때 받은 난수값을 적어두기
        _uniqueNumberText.text = 50.ToString(); 
    }
}
