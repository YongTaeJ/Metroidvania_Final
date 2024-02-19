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
    #endregion

    public void Initialize(int ID)
    {
        _ID = ID;
        base.Initialize();
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
    }

    private void BuildingConstructAnimation(int buildingID)
    {
        var buildingSO = SOManager.Instance.GetBuildingSO(buildingID);

        GameObject buildingPrefab = buildingSO.BuildingData.buildingPrefab;
        Vector3 buildingPosition = buildingSO.BuildingData.buildingPosition;
        GameObject buildingObject = Instantiate(buildingPrefab, buildingPosition, Quaternion.identity);
        Animator animator = buildingObject.GetComponent<Animator>();
        animator.SetBool("IsConstruct", true);

        HideUI(true);

        CameraManager.Instance.ChangeCameraTarget(buildingObject.gameObject.transform);

        StartCoroutine(Recover(buildingObject));
    }

    private void HideUI(bool hide)
    {
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in canvases)
        {
            canvas.enabled = !hide;
        }
    }

    private IEnumerator Recover(GameObject obj, float delay = 0f)
    {
        Animator animator = obj.GetComponent<Animator>();

        if (animator != null)
        {
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            delay = clipInfo[0].clip.length;
        }
        yield return new WaitForSeconds(delay + 1f);
        HideUI(false);
        CameraManager.Instance.ReturnCameraTarget();
        Destroy(gameObject);
    }
}
