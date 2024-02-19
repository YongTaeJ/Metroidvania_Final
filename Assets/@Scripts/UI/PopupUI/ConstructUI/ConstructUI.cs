using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructUI : MonoBehaviour
{
    public ConstructInformPanel InformPanel { get; private set; }
    public ConstructList BuildingList { get; private set; }

    private void Awake() 
    {

        Button button = transform.Find("ExitButton").GetComponent<Button>();
        button.onClick.AddListener( () => { gameObject.SetActive(false);});

        InformPanel = GetComponentInChildren<ConstructInformPanel>();
        BuildingList = GetComponentInChildren<ConstructList>();

        InformPanel.Initialize(this);
        BuildingList.Initialize(this);
    }
}
