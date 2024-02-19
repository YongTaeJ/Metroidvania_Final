using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillUI : MonoBehaviour
{
    private Dictionary<int, GameObject> skillUIObjects = new Dictionary<int, GameObject>();

    private void Start()
    {
        InitializeSkillUI();
        UpdateSkillUI();
    }


    private void InitializeSkillUI()
    {
       
        foreach (Transform child in transform)
        {
            int skillId = GetSkillI(child.name);
            if (skillId != -1) 
            {
                skillUIObjects[skillId] = child.gameObject;
                child.gameObject.SetActive(false);
            }
        }
    }

    private int GetSkillI(string name)
    {
        if (name.Contains("Skill_SwordAuror"))
        {
            return 0;
        }
        else if (name.Contains("Skill_PlungeAttack"))
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    private void UpdateSkillUI()
    {
        foreach (var skillUI in skillUIObjects)
        {
            int skillId = skillUI.Key;
            GameObject uiObject = skillUI.Value;
            uiObject.SetActive(ItemManager.Instance.HasItem(ItemType.Skill, skillId));
        }
    }

    public void RefreshSkills()
    {
        UpdateSkillUI();
    }
}
