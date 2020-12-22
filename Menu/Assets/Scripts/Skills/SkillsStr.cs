using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsStr : MonoBehaviour
{
    public GameObject bullet;
    public Transform actionPoint;
    public GameObject explodeBullet;

    public void IncreaseDMG()
    {
        GetComponent<SkillCooldown>().strCooldown = GetComponent<SkillCooldown>().strCooldownTime;
        GetComponent<SkillCooldown>().strCooldownWait = GetComponent<SkillCooldown>().strCooldownTimeWait;
        randomDMG();
        GetComponent<PlayerMovement>().attackDamage = 50;
    }

    public void randomDMG()
    {
        int random = new System.Random().Next(65, 125);
        GetComponent<PlayerMovement>().attackDamage = random;
    }

    public void ShootBullet()
    {
        GetComponent<SkillCooldown>().strBulletCooldown = GetComponent<SkillCooldown>().strBulletCooldownTime;
        Instantiate(bullet, actionPoint.position, actionPoint.rotation).SetActive(true);
    }


    public void Explode()
    {
        GetComponent<SkillCooldown>().strExplodeCooldown = GetComponent<SkillCooldown>().strExplodeCooldownTime;
        GameObject explode = Instantiate(explodeBullet, transform.position, transform.rotation);
        explode.SetActive(true);
    }



}
