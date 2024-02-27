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
        OnRefresh -= constructUI.InformPanel.Refresh;
        OnRefresh += constructUI.InformPanel.Refresh;
        OnRefresh -= constructUI.BuildingList.RefreshValidButtons;
        OnRefresh += constructUI.BuildingList.RefreshValidButtons;
    }

    protected override void OnClickNo()
    {
        Destroy(gameObject);
    }

    protected override void OnClickYes()
    {
        var SO = SOManager.Instance.GetBuildingSO(_ID);

        foreach(var item in SO.RequiredMaterials)
        {
            ItemManager.Instance.UseItem(ItemType.Material, item.ID, item.Stock);
        }

        ItemManager.Instance.AddItem(ItemType.Building, _ID);
        SO.GiveReward();

        BuildingConstructAnimation();

        OnRefresh.Invoke();
    }

    private void BuildingConstructAnimation()
    {
        var buildingSO = SOManager.Instance.GetBuildingSO(_ID);

        GameObject buildingPrefab = buildingSO.BuildingData.buildingPrefab;
        Vector3 buildingPosition = buildingSO.BuildingData.buildingPosition;
        GameObject buildingObject = Instantiate(buildingPrefab, buildingPosition, Quaternion.identity);
        Animator animator = buildingObject.GetComponent<Animator>();
        animator.SetBool("IsConstruct", true);

        HideUI(true);

        CameraManager.Instance.ChangeCameraTarget(buildingObject.gameObject.transform);

        StartCoroutine(Recover(buildingObject, 1000 + _ID));
    }

    private void HideUI(bool hide)
    {
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in canvases)
        {
            canvas.enabled = !hide;
        }
    }

    private IEnumerator Recover(GameObject obj, int chatID, float delay = 0f)
    {
        Animator animator = obj.GetComponent<Animator>();

        if (animator != null)
        {
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            delay = clipInfo[0].clip.length;
        }
        yield return new WaitForSeconds(delay + 3f);
        HideUI(false);
        CameraManager.Instance.ReturnCameraTarget();

        // TODO => Disposable로 바꾼 뒤에 처리
        // yield return StartCoroutine(ChatManager.Instance.StartChat(chatID));
        
        Destroy(gameObject);
    }
}
