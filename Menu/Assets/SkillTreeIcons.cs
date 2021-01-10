using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeIcons : MonoBehaviour
{
    public GameObject increaseDmg;
    public GameObject bullet;
    public GameObject explode;
    public GameObject regen;
    public GameObject immorality;
    public GameObject player;
    void Start()
    {
        checkSkills();
    }

    public void checkSkills()
    {
        increaseDmg.SetActive(player.GetComponent<PlayerMovement>().CanUseIncreaseDMG());
        bullet.SetActive(player.GetComponent<PlayerMovement>().canUseBullet());
        explode.SetActive(player.GetComponent<PlayerMovement>().CanUseExplode());
        regen.SetActive(player.GetComponent<PlayerMovement>().CanUseRegenHP());
        immorality.SetActive(player.GetComponent<PlayerMovement>().CanUseImmortality());
    }
}
