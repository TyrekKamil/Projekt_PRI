﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills
{
    public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;
    public class OnSkillUnlockedEventArgs : EventArgs {
        public SkillType skillType;
    }
    public enum SkillType { 
        None,
        //Blue Skills
        Dash,
        Blank,
        BlueSkill3,
        BlueSkill4,
        BlueSkill5,
        //Red Skills
        RedSkill1,
        RedSkill2,
        RedSkill3,
        RedSkill4,
        RedSkill5,
        //Green Skills
        GreenSkill1,
        GreenSkill2,
        GreenSkill3,
        GreenSkill4,
        GreenSkill5,
    }
    private List<SkillType> unlockedSkillTypeList;

    public PlayerSkills() {
        unlockedSkillTypeList = new List<SkillType>();
    }
    private void UnlockSkill(SkillType skillType) {
        // later add  && GLOBAL_DATA.Instance.Level - 1 > unlockedSkillTypeList.Count
        if (!IsSkillTypeUnlocked(skillType))
        {
            unlockedSkillTypeList.Add(skillType);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs {skillType = skillType});
        }
    }
    public bool IsSkillTypeUnlocked(SkillType skillType) {
        return unlockedSkillTypeList.Contains(skillType);
    }
    public bool CanUnlock(SkillType skillType) {
        SkillType skillRequirement = GetSkillRequirement(skillType);

        if (skillRequirement != SkillType.None)
        {
            if (IsSkillTypeUnlocked(skillRequirement))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }
    public SkillType GetSkillRequirement(SkillType skillType) {
        switch (skillType) {
            //Blue Skills Requirements
            case SkillType.Dash: return SkillType.Blank;
            case SkillType.BlueSkill3: return SkillType.Dash;
            case SkillType.BlueSkill4: return SkillType.BlueSkill3;
            case SkillType.BlueSkill5: return SkillType.BlueSkill4;
            //Red Skills Requirements
            case SkillType.RedSkill2: return SkillType.RedSkill1;
            case SkillType.RedSkill3: return SkillType.RedSkill2;
            case SkillType.RedSkill4: return SkillType.RedSkill3;
            case SkillType.RedSkill5: return SkillType.RedSkill4;
            //Green Skills Requirements
            case SkillType.GreenSkill2: return SkillType.GreenSkill1;
            case SkillType.GreenSkill3: return SkillType.GreenSkill2;
            case SkillType.GreenSkill4: return SkillType.GreenSkill3;
            case SkillType.GreenSkill5: return SkillType.GreenSkill4;
        }
        return SkillType.None;
    }
    public bool TryUnlockingSkill(SkillType skillType) {
        if (CanUnlock(skillType)) {
            UnlockSkill(skillType);
            return true;
        }
        return false;
    }
}
