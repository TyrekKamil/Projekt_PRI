﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldown : MonoBehaviour
{

    // STR Skills
    public float strCooldown = 0f;
    public float strCooldownTime = 10f;

    public float strCooldownWait = 0f;
    public float strCooldownTimeWait = 15f;
    public float strBulletCooldown = 0f;
    public float strBulletCooldownTime = 15f;
    public float strExplodeCooldown = 0f;
    public float strExplodeCooldownTime = 5f;
    public GameObject strBulletLoadingBar;
    public GameObject strIncreaseLoadingBar;
    public GameObject strExplodeLoadingBar;

    public GameObject patRegenLoadingBar;
    public GameObject patImmortalityLoadingBar;


    // PAT skills
    public float patImmortalityCooldown = 0f;
    public float patImmortalityCooldownTime = 50f;
    public float patRegenerationCooldown = 0f;
    public float patRegenerationCooldownTime = 30f;

    void Update()
    {
        strSkills();
        patSkills();
    }

    private void increaseStrActivatedCooldown()
    {
        if (strCooldown > 0)
        {
            strCooldown -= Time.deltaTime;
        }
        if (strCooldown < 0)
        {
            strCooldown = 0;
        }
        if (strCooldown == 0 && GetComponent<PlayerMovement>().increaseDMGSkillActivated == true)
        {
            GetComponent<PlayerMovement>().increaseDMGSkillActivated = false;
            GetComponent<PlayerMovement>().attackDamage = 50;
            Debug.Log("Deactivated");
        }
    }
    private void increaseStrCooldown()
    {
        if (strCooldownWait > 0)
        {
            strCooldownWait -= Time.deltaTime;
        }
        if (strCooldownWait < 0)
        {
            strCooldownWait = 0;
        }
        if (strCooldownWait == 0 && GetComponent<PlayerMovement>().canUseIncreaseStr == false)
        {
            GetComponent<PlayerMovement>().canUseIncreaseStr = true;
        }
        strIncreaseLoadingBar.GetComponent<Image>().fillAmount = (strCooldownTimeWait - strCooldownWait) / strCooldownTimeWait;
    }
    private void bulletCooldown()
    {
        if (strBulletCooldown > 0)
        {
            strBulletCooldown -= Time.deltaTime;
        }
        if (strBulletCooldown < 0)
        {
            strBulletCooldown = 0;
        }
        if (strBulletCooldown == 0 && GetComponent<PlayerMovement>().canUseBulletSkill == false)
        {
            GetComponent<PlayerMovement>().canUseBulletSkill = true;
        }
        strBulletLoadingBar.GetComponent<Image>().fillAmount = (strBulletCooldownTime - strBulletCooldown) / strBulletCooldownTime;
    }
    private void explodeCooldown()
    {
        if (strExplodeCooldown > 0)
        {
            strExplodeCooldown -= Time.deltaTime;
        }
        if (strExplodeCooldown < 0)
        {
            strExplodeCooldown = 0;
        }
        if (strExplodeCooldown == 0 && GetComponent<PlayerMovement>().canUseExplodeSkill == false)
        {
            GetComponent<PlayerMovement>().canUseExplodeSkill = true;
        }
        strExplodeLoadingBar.GetComponent<Image>().fillAmount = (strExplodeCooldownTime - strExplodeCooldown) / strExplodeCooldownTime;
    }

    private void immortalityCooldown()
    {
        if (patImmortalityCooldown > 0)
        {
            patImmortalityCooldown -= Time.deltaTime;
        }
        if (patImmortalityCooldown < 0)
        {
            patImmortalityCooldown = 0;
        }
        if (patImmortalityCooldown == 0 && GetComponent<PlayerMovement>().isImmortalityActivated)
        {
            GetComponent<PlayerMovement>().isImmortalityActivated = false;
        }
        patImmortalityLoadingBar.GetComponent<Image>().fillAmount = (patImmortalityCooldownTime - patImmortalityCooldown) / patImmortalityCooldownTime;
    }

    private void regenerationCooldown()
    {
        if (patRegenerationCooldown > 0)
        {
            patRegenerationCooldown -= Time.deltaTime;
        }
        if (patRegenerationCooldown < 0)
        {
            patRegenerationCooldown = 0;
        }
        if (patRegenerationCooldown == 0 && GetComponent<PlayerMovement>().isRegenerationHpActivated)
        {
            GetComponent<PlayerMovement>().isRegenerationHpActivated = false;
        }
        patRegenLoadingBar.GetComponent<Image>().fillAmount = (patRegenerationCooldownTime - patRegenerationCooldown) / patRegenerationCooldownTime;

    }

    private void strSkills()
    {
        increaseStrActivatedCooldown();
        increaseStrCooldown();
        bulletCooldown();
        explodeCooldown();
    }

    private void patSkills()
    {
        immortalityCooldown();
        regenerationCooldown();
    }
}
