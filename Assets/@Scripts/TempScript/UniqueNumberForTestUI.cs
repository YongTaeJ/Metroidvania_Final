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
        int randomUniqueNumber = GameManager.Instance.Data.randomUniqueNumber;
        _uniqueNumberText.text = randomUniqueNumber.ToString();
    }
}
