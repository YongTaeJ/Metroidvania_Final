using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    public BuildingInformPanel BuildingInformPanel {get; private set;}
    public BuildingList BuildingList {get; private set;}

    private void Awake()
    {
        BuildingInformPanel = transform.Find("DataDependent/InformPanel").GetComponent<BuildingInformPanel>();
        BuildingList = transform.Find("DataDependent/BuildingList").GetComponent<BuildingList>();

        BuildingList.Initialize(this);

        Button exitButton = transform.Find("ExitButton").GetComponent<Button>();
        exitButton.onClick.AddListener(() => gameObject.SetActive(false) );
    }
}
