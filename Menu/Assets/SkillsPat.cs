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
    public void IncreasedHpPermamentlySkill()
    {
        playerData.maxHealth = 125;
        playerData.slider.SetMaxHealth(125);
        Statics.isHpBoostedFromSkill = true;
    }

    public void RegenHP(int hpRegenerated)
    {
        skillCooldown.patRegenerationCooldown = skillCooldown.patRegenerationCooldownTime;
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
        StartCoroutine("WaitForSec");
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        if (Statics.isImmortal)
        {
            Statics.isImmortal = false;
        }

    }
}
