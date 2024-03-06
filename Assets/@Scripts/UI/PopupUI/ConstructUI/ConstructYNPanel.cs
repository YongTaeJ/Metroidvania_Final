using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConstructYNPanel : YNPanel
{
    #region variables
    private int _currentID;
    private Action OnRefresh;
    private ConstrcutIndicatorContainer _indicatorContainer;
    #endregion

    public void Initialize(ConstructUI constructUI)
    {
        base.Initialize();
        InitAction(constructUI);

        _indicatorContainer = GetComponentInChildren<ConstrcutIndicatorContainer>();
        _indicatorContainer.Initialize();
        gameObject.SetActive(false);
    }

    private void InitAction(ConstructUI constructUI)
    {
        OnRefresh -= constructUI.InformPanel.Refresh;
        OnRefresh += constructUI.InformPanel.Refresh;
        OnRefresh -= constructUI.BuildingList.RefreshValidButtons;
        OnRefresh += constructUI.BuildingList.RefreshValidButtons;
    }

    public void OpenPanel(int ID)
    {
        _currentID = ID;
        _indicatorContainer.SetIndicators(ID);
        gameObject.SetActive(true);
    }

    protected override void OnClickNo()
    {
        Destroy(gameObject);
    }

    protected override void OnClickYes()
    {
        var SO = SOManager.Instance.GetBuildingSO(_currentID);

        foreach(var item in SO.RequiredMaterials)
        {
            ItemManager.Instance.UseItem(ItemType.Material, item.ID, item.Stock);
        }

        ItemManager.Instance.AddItem(ItemType.Building, _currentID);
        SO.GiveReward();

        BuildingConstructAnimation();

        OnRefresh.Invoke();
    }

    private void BuildingConstructAnimation()
    {
        var buildingSO = SOManager.Instance.GetBuildingSO(_currentID);

        GameObject buildingPrefab = buildingSO.BuildingData.buildingPrefab;
        Vector3 buildingPosition = buildingSO.BuildingData.buildingPosition;
        GameObject buildingObject = Instantiate(buildingPrefab, buildingPosition, Quaternion.identity);
        SFX.Instance.PlayOneShot(ResourceManager.Instance.GetAudioClip("Construct"));
        Animator animator = buildingObject.GetComponent<Animator>();
        animator.SetBool("IsConstruct", true);

        UIManager.Instance.SetEventMode(true);

        CameraManager.Instance.ChangeCameraTarget(buildingObject.gameObject.transform);

        StartCoroutine(Recover(buildingObject, 1000 + _currentID));
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

        SFX.Instance.PlayOneShot("ConstructComplete");

        EventChatBoxUI chatBox = UIManager.Instance.GetDisposableUI<EventChatBoxUI>(DisposableType.EventChatBox);
        chatBox.Initialize();

        var chatDatas = ChatManager.Instance.GetChatData(chatID);

        yield return StartCoroutine(chatBox.StartChat(chatDatas));

        chatBox.EndChat();

        UIManager.Instance.SetEventMode(false);
        CameraManager.Instance.ReturnCameraTarget();

        gameObject.SetActive(false);
    }
}
