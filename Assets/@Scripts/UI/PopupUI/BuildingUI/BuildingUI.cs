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

        // ExitButton
        Button exitButton = transform.Find("ExitButton").GetComponent<Button>();
        exitButton.onClick.AddListener(() => gameObject.SetActive(false) );

        // ToBuildingButton
        Button toInventoryUIButton = transform.Find("ToInventoryUIButton").GetComponent<Button>();
        toInventoryUIButton.onClick.AddListener
        ( () =>
        {
            UIManager.Instance.OpenPopupUI(PopupType.Inventory);
            gameObject.SetActive(false);
        }
        );
    }
}
