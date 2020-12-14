using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class UI_SkillTree : MonoBehaviour
{
    private PlayerSkills playerSkills;
    private void Awake()
    {
        transform.Find("BlueSkills").Find("dashButton").GetComponent<Button_UI>().ClickFunc = () =>
        {
            if (!playerSkills.TryUnlockingSkill(PlayerSkills.SkillType.Dash))
            {
                //place for the warning box initiation
            }

        };
        transform.Find("BlueSkills").Find("blankButton").GetComponent<Button_UI>().ClickFunc = () =>
        {
            if (!playerSkills.TryUnlockingSkill(PlayerSkills.SkillType.Blank)) {
                //place for the warning box initiation
            }

        };
    }

    public void SetPlayerSkills(PlayerSkills playerSkills) {
        this.playerSkills = playerSkills;
    }
}
