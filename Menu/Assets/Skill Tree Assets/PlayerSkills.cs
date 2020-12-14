﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills
{
    public enum SkillType { 
        None,
        Dash,
        Blank
    }
    private List<SkillType> unlockedSkillTypeList;

    public PlayerSkills() {
        unlockedSkillTypeList = new List<SkillType>();
    }
    private void UnlockSkill(SkillType skillType) {
        if(!IsSkillTypeUnlocked(skillType))
            unlockedSkillTypeList.Add(skillType);
    }
    public bool IsSkillTypeUnlocked(SkillType skillType) {
        return unlockedSkillTypeList.Contains(skillType);
    }

    public SkillType GetSkillRequirement(SkillType skillType) {
        switch (skillType) {
            case SkillType.Blank: return SkillType.Dash;
        }
        return SkillType.None;
    }
    public bool TryUnlockingSkill(SkillType skillType) {
        SkillType skillRequirement = GetSkillRequirement(skillType);
        if (skillRequirement != SkillType.None)
        {
            if (IsSkillTypeUnlocked(skillRequirement))
            {
                UnlockSkill(skillType);
                Debug.Log(skillType.ToString() + " successfully unlocked.");
                return true;
            }
            else
            {
                Debug.Log("Cannot unlock skill. " + skillRequirement.ToString() + " isn't unlocked yet.");
                return false;
            }
        }
        else {
            UnlockSkill(skillType);
            Debug.Log(skillType.ToString() + " successfully unlocked.");
            return true;
        }
    }
}
