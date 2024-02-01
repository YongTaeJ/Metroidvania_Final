using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// 리펙토링 필요
/// </summary>
public class SkillUI : MonoBehaviour
{
    private GameObject SwordAuror;
    private GameObject PlungeAttack;
    private void Start()
    {
        FindChildObject();
    }

    private void Update()
    {
        //ActivateChildObject();
    }

    private void FindChildObject()
    {
        SwordAuror = transform.Find("Skill_SwordAuror").gameObject;
        PlungeAttack = transform.Find("Skill_PlungeAttack").gameObject;

        if (SwordAuror != null)
        {
            SwordAuror.SetActive(false);
        }
        if (PlungeAttack != null)
        {
            PlungeAttack.SetActive(false);
        }
    }

    private void ActivateChildObject()
    {
        if (SwordAuror != null && ItemManager.Instance.HasItem(ItemType.Skill, 0))
        {
            SwordAuror.SetActive(true);
        }
        if (PlungeAttack != null && ItemManager.Instance.HasItem(ItemType.Skill, 1))
        {
            PlungeAttack.SetActive(true);
        }
    }
}
