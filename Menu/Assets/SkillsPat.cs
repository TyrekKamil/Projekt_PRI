using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsPat : MonoBehaviour
{
    private PlayerUIUpdates playerData;
    private SkillCooldown skillCooldown;

    private void Start()
    {
        playerData = GetComponent<PlayerUIUpdates>();
        skillCooldown = GetComponent<SkillCooldown>();
    }

    private void Update()
    {
        Debug.Log(skillCooldown.patImmortalityCooldown + "cooldown to immortlaity");
        Debug.Log(Statics.isImmortal);
    }
    public void IncreasedHpPermamentlySkill()
    {
        playerData.maxHealth = 150;
        playerData.slider.SetMaxHealth(150);
        Debug.Log("HP increased from 100 to 150!");
    }

    public void RegenHP(int hpRegenerated)
    {
        for (int i = 0; i < hpRegenerated; i++)
        {
            if (playerData.currentHealth == playerData.maxHealth)
                return;

            playerData.currentHealth += 1;
        }
    }

    public void Immortality(bool option)
    {
        skillCooldown.patImmortalityCooldown = skillCooldown.patImmortalityCooldownTime;
        Statics.isImmortal = option;
    }

}
