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
            playerSkills.TryUnlockingSkill(PlayerSkills.SkillType.Dash);
        };
    }

    public void SetPlayerSkills(PlayerSkills playerSkills) {
        this.playerSkills = playerSkills;
    }
}
