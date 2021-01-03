using System;
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
        Sprint,
        TripleJump,
        Dash,
        //Red Skills
        IncreaseDMG,
        Bullet,
        Explode,
        //Green Skills
        ExtraHP,
        RegenerationHP,
        Immortality,
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
            case SkillType.TripleJump: return SkillType.Sprint;
            case SkillType.Dash: return SkillType.TripleJump;
            //Red Skills Requirements
            case SkillType.Bullet: return SkillType.IncreaseDMG;
            case SkillType.Explode: return SkillType.Bullet;
            //Green Skills Requirements
            case SkillType.RegenerationHP: return SkillType.ExtraHP;
            case SkillType.Immortality: return SkillType.RegenerationHP;
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
