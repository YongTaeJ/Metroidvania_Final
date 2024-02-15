using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConstructYNPanel : YNPanel
{
    #region variables
    private int _ID;
    private Action OnRefresh;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    #endregion

    public void Initialize(int ID)
    {
        _ID = ID;
        base.Initialize();
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    public void InitAction(ConstructUI constructUI)
    {
        OnRefresh -= constructUI.BuildingList.RefreshValidButtons;
        OnRefresh += constructUI.BuildingList.RefreshValidButtons;
    }

    protected override void OnClickNo()
    {
        Destroy(gameObject);
    }

    protected override void OnClickYes()
    {
        // TODO => 차후순위로 건축 기념 이벤트 혹은 UI를 넣을 수 있을 듯.
        var SO = SOManager.Instance.GetBuildingSO(_ID);

        foreach(var item in SO.RequiredMaterials)
        {
            ItemManager.Instance.UseItem(ItemType.Material, item.ID, item.Stock);
        }

        ItemManager.Instance.AddItem(ItemType.Building, _ID);

        BuildingConstructAnimation(_ID);

        OnRefresh.Invoke();

        Destroy(gameObject);
    }

    private void BuildingConstructAnimation(int buildingID)
    {
        var buildingSO = SOManager.Instance.GetBuildingSO(buildingID);
        Transform originalTarget = cinemachineVirtualCamera.Follow;

        GameObject buildingPrefab = buildingSO._buildingData.buildingPrefab;
        Vector3 buildingPosition = buildingSO._buildingData.buildingPosition;
        GameObject buildingObject = Instantiate(buildingPrefab, buildingPosition, Quaternion.identity);

        HideUI(true);
        ChangeCinemachineCameraTarget(buildingPosition);

        StartCoroutine(WaitForAnimation(buildingObject));

        cinemachineVirtualCamera.Follow = originalTarget;
    }
    public void HideUI(bool hide)
    {
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in canvases)
        {
            canvas.enabled = !hide;
        }
    }

    private void ChangeCinemachineCameraTarget(Vector3 Position)
    {
        Transform buildingTransform = new GameObject("BuildingTarget").transform;
        buildingTransform.position = Position;
        cinemachineVirtualCamera.Follow = buildingTransform;
    }

    IEnumerator WaitForAnimation(GameObject buildingPrefab, float waitTime = 0f)
    {
        Animator animator = buildingPrefab.GetComponent<Animator>();

        if (animator != null)
        {
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            waitTime = clipInfo[0].clip.length;
            Debug.Log(waitTime);
        }

        yield return new WaitForSeconds(1f);
        Debug.Log("Return");

        HideUI(false);
    }
}
