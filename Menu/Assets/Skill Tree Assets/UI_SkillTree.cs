using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
public class UI_SkillTree : MonoBehaviour
{
    
    [SerializeField] private Material skillLockedMaterial;
    [SerializeField] private Material skillUnlockableMaterial;
    [SerializeField] private SkillUnlockPath[] skillUnlkockPathArray;

    private PlayerSkills playerSkills;
    private List<SkillButton> skillButtonList;

    string blueColor = "BlueSkills";
    string redColor = "RedSkills";
    string greenColor = "GreenSkills";
    public void SetPlayerSkills(PlayerSkills playerSkills) {
        this.playerSkills = playerSkills;

        skillButtonList = new List<SkillButton>();
        //Add Blue Skills
        skillButtonList.Add(new SkillButton(transform.Find("BlueSkills").Find("dashButton"), playerSkills, PlayerSkills.SkillType.Dash, skillLockedMaterial, skillUnlockableMaterial , "Player dashes horizontally in the current direction."));
        skillButtonList.Add(new SkillButton(transform.Find("BlueSkills").Find("blankButton"), playerSkills, PlayerSkills.SkillType.Blank, skillLockedMaterial, skillUnlockableMaterial, "Allows you to use jump once more in the air"));
        skillButtonList.Add(new SkillButton(transform.Find("BlueSkills").Find("blueButton"), playerSkills, PlayerSkills.SkillType.BlueSkill3, skillLockedMaterial, skillUnlockableMaterial, ""));
        skillButtonList.Add(new SkillButton(transform.Find("BlueSkills").Find("blueButton2"), playerSkills, PlayerSkills.SkillType.BlueSkill4, skillLockedMaterial, skillUnlockableMaterial, ""));
        skillButtonList.Add(new SkillButton(transform.Find("BlueSkills").Find("blueButton3"), playerSkills, PlayerSkills.SkillType.BlueSkill5, skillLockedMaterial, skillUnlockableMaterial, ""));
        //Add Red Skills
        skillButtonList.Add(new SkillButton(transform.Find("RedSkills").Find("increaseDMGButton"), playerSkills, PlayerSkills.SkillType.IncreaseDMG, skillLockedMaterial, skillUnlockableMaterial, ""));
        skillButtonList.Add(new SkillButton(transform.Find("RedSkills").Find("bulletButton"), playerSkills, PlayerSkills.SkillType.Bullet, skillLockedMaterial, skillUnlockableMaterial, ""));
        skillButtonList.Add(new SkillButton(transform.Find("RedSkills").Find("explodeButton"), playerSkills, PlayerSkills.SkillType.Explode, skillLockedMaterial, skillUnlockableMaterial, ""));
        skillButtonList.Add(new SkillButton(transform.Find("RedSkills").Find("redButton4"), playerSkills, PlayerSkills.SkillType.RedSkill4, skillLockedMaterial, skillUnlockableMaterial, ""));
        skillButtonList.Add(new SkillButton(transform.Find("RedSkills").Find("redButton5"), playerSkills, PlayerSkills.SkillType.RedSkill5, skillLockedMaterial, skillUnlockableMaterial, ""));
        //Add Green Skills
        skillButtonList.Add(new SkillButton(transform.Find("GreenSkills").Find("extraHpButton"), playerSkills, PlayerSkills.SkillType.ExtraHP, skillLockedMaterial, skillUnlockableMaterial, ""));
        skillButtonList.Add(new SkillButton(transform.Find("GreenSkills").Find("regenerationHpButton"), playerSkills, PlayerSkills.SkillType.RegenerationHP, skillLockedMaterial, skillUnlockableMaterial, ""));
        skillButtonList.Add(new SkillButton(transform.Find("GreenSkills").Find("immortalityButton"), playerSkills, PlayerSkills.SkillType.Immortality, skillLockedMaterial, skillUnlockableMaterial, ""));
        skillButtonList.Add(new SkillButton(transform.Find("GreenSkills").Find("greenButton4"), playerSkills, PlayerSkills.SkillType.GreenSkill4, skillLockedMaterial, skillUnlockableMaterial, ""));
        skillButtonList.Add(new SkillButton(transform.Find("GreenSkills").Find("greenButton5"), playerSkills, PlayerSkills.SkillType.GreenSkill5, skillLockedMaterial, skillUnlockableMaterial, ""));
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

        foreach (SkillUnlockPath skillUnlockPath in skillUnlkockPathArray) {
            foreach (Image linkImage in skillUnlockPath.linkImageArray) {
                linkImage.color = new Color(.5f, .5f, .5f);
            }
        }

        foreach (SkillUnlockPath skillUnlockPath in skillUnlkockPathArray)
        {
            if (playerSkills.IsSkillTypeUnlocked(skillUnlockPath.skillType) || playerSkills.CanUnlock(skillUnlockPath.skillType)) {
                foreach (Image linkImage in skillUnlockPath.linkImageArray)
                {
                    linkImage.color = Color.white;
                }
            }

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
        private string description;

        public SkillButton(Transform transform, PlayerSkills playerSkills, PlayerSkills.SkillType skillType, Material skillLockedMaterial, Material skillUnlockableMaterial, string description) {
            this.transform = transform;
            this.playerSkills = playerSkills;
            this.skillType = skillType;
            this.description = description;
            this.skillLockedMaterial = skillLockedMaterial;
            this.skillUnlockableMaterial = skillUnlockableMaterial;

            image = transform.Find("Image").GetComponent<Image>();
            backgroundImage = transform.Find("background").GetComponent<Image>();
            transform.GetComponent<Button_UI>().MouseOverOnceFunc = () => Tooltip.ShowTooltip_Static(description);
            transform.GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();

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

    [System.Serializable]
    public class SkillUnlockPath
    {
        public PlayerSkills.SkillType skillType;
        public Image[] linkImageArray;
    }
}
