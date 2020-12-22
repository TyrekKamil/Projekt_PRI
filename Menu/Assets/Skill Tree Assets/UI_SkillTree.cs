using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
public class UI_SkillTree : MonoBehaviour
{
    
    [SerializeField] private Material skillLockedMaterial;
    [SerializeField] private Material skillUnlockableMaterial;

    private PlayerSkills playerSkills;
    private List<SkillButton> skillButtonList;

    string blueColor = "BlueSkills";
    string redColor = "RedSkills";
    string greenColor = "GreenSkills";
    public void SetPlayerSkills(PlayerSkills playerSkills) {
        this.playerSkills = playerSkills;

        skillButtonList = new List<SkillButton>();
        //Add Blue Skills
        skillButtonList.Add(new SkillButton(transform.Find("BlueSkills").Find("dashButton"), playerSkills, PlayerSkills.SkillType.Dash, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("BlueSkills").Find("blankButton"), playerSkills, PlayerSkills.SkillType.Blank, skillLockedMaterial, skillUnlockableMaterial));
        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
        UpdateVisuals();
    }
    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e) {
        UpdateVisuals();
    }
    private void UpdateVisuals() {
        foreach (SkillButton skillButton in skillButtonList) {
            skillButton.UpdateVisual();
        }
    }
    
    private class SkillButton {
        private Transform transform;
        private Image image;
        private Image backgroundImage;
        private PlayerSkills playerSkills;
        private PlayerSkills.SkillType skillType;
        private Material skillLockedMaterial;
        private Material skillUnlockableMaterial;

        public SkillButton(Transform transform, PlayerSkills playerSkills, PlayerSkills.SkillType skillType, Material skillLockedMaterial, Material skillUnlockableMaterial) {
            this.transform = transform;
            this.playerSkills = playerSkills;
            this.skillType = skillType;
            this.skillLockedMaterial = skillLockedMaterial;
            this.skillUnlockableMaterial = skillUnlockableMaterial;

            image = transform.Find("Image").GetComponent<Image>();
            backgroundImage = transform.Find("background").GetComponent<Image>();

            transform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                if (!playerSkills.TryUnlockingSkill(skillType))
                {
                    //place for the warning box initiation
                }
            };
        }
        public void UpdateVisual()
        {
            if (playerSkills.IsSkillTypeUnlocked(skillType))
            {
                image.material = null;
                backgroundImage.material = null;
            }
            else
            {
                if (playerSkills.CanUnlock(skillType))
                {
                    image.material = skillUnlockableMaterial;
                    backgroundImage.color = UtilsClass.GetColorFromString("A0D0FF");
                    transform.GetComponent<Button_UI>().enabled = true;
                }
                else
                {
                    image.material = skillLockedMaterial;
                    backgroundImage.color = new Color(.3f,.3f,.3f);
                    transform.GetComponent<Button_UI>().enabled = false;
                }
            }
        }
    }
}
