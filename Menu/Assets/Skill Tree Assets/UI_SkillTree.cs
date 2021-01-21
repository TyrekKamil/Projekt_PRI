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
    private Transform skillpoints;
    string blueColor = "BlueSkills";
    string redColor = "RedSkills";
    string greenColor = "GreenSkills";
    static int unlockedSkills = 0;
    int previousSkillValue = 0;
    public GameObject skillIcons;
    private void Update()
    {
        if (previousSkillValue != GLOBAL_DATA.Instance.Level - 1 - unlockedSkills) {
            skillpoints.GetComponent<UnityEngine.UI.Text>().text = (GLOBAL_DATA.Instance.Level - 1 - unlockedSkills).ToString();
            previousSkillValue = GLOBAL_DATA.Instance.Level - 1 - unlockedSkills;
        }
    }
    public void SetPlayerSkills(PlayerSkills playerSkills) {
        this.playerSkills = playerSkills;

        skillButtonList = new List<SkillButton>();
        //Add Blue Skills
        skillButtonList.Add(new SkillButton(transform.Find("BlueSkills").Find("sprintButton"), playerSkills, PlayerSkills.SkillType.Sprint, skillLockedMaterial, skillUnlockableMaterial, "Allows you to move faster by holding CTRL"));
        skillButtonList.Add(new SkillButton(transform.Find("BlueSkills").Find("tripleJumpButton"), playerSkills, PlayerSkills.SkillType.TripleJump, skillLockedMaterial, skillUnlockableMaterial, "Allows you to use jump once more in the air"));
        skillButtonList.Add(new SkillButton(transform.Find("BlueSkills").Find("dashButton"), playerSkills, PlayerSkills.SkillType.Dash, skillLockedMaterial, skillUnlockableMaterial , "Player dashes horizontally in the current direction."));
        //Add Red Skills
        skillButtonList.Add(new SkillButton(transform.Find("RedSkills").Find("increaseDMGButton"), playerSkills, PlayerSkills.SkillType.IncreaseDMG, skillLockedMaterial, skillUnlockableMaterial, "Increase your damage to 70-120 for 10 seconds"));
        skillButtonList.Add(new SkillButton(transform.Find("RedSkills").Find("bulletButton"), playerSkills, PlayerSkills.SkillType.Bullet, skillLockedMaterial, skillUnlockableMaterial, "Allows you to launch a bullet, that defeats enemy"));
        skillButtonList.Add(new SkillButton(transform.Find("RedSkills").Find("explodeButton"), playerSkills, PlayerSkills.SkillType.Explode, skillLockedMaterial, skillUnlockableMaterial, "Allows you to explode, that elimitate enemies."));
        //Add Green Skills
        skillButtonList.Add(new SkillButton(transform.Find("GreenSkills").Find("extraHpButton"), playerSkills, PlayerSkills.SkillType.ExtraHP, skillLockedMaterial, skillUnlockableMaterial, "Adds permamently 25 more health points"));
        skillButtonList.Add(new SkillButton(transform.Find("GreenSkills").Find("regenerationHpButton"), playerSkills, PlayerSkills.SkillType.RegenerationHP, skillLockedMaterial, skillUnlockableMaterial, "Regenerates 15 hp over time"));
        skillButtonList.Add(new SkillButton(transform.Find("GreenSkills").Find("immortalityButton"), playerSkills, PlayerSkills.SkillType.Immortality, skillLockedMaterial, skillUnlockableMaterial, "You are immortal for 2 seconds"));
        skillpoints = transform.Find("skill points").Find("Text");
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
        unlockedSkills = 0;
        foreach (SkillUnlockPath skillUnlockPath in skillUnlkockPathArray)
        {
            if (playerSkills.IsSkillTypeUnlocked(skillUnlockPath.skillType)) {
                unlockedSkills += 1;
            }
            if (playerSkills.IsSkillTypeUnlocked(skillUnlockPath.skillType) || playerSkills.CanUnlock(skillUnlockPath.skillType)) {
                foreach (Image linkImage in skillUnlockPath.linkImageArray)
                {
                    linkImage.color = Color.white;
                }
            }

        }
        skillpoints.GetComponent<UnityEngine.UI.Text>().text = (GLOBAL_DATA.Instance.Level - 1 - unlockedSkills).ToString();
        skillIcons.GetComponent<SkillTreeIcons>().checkSkills();
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
