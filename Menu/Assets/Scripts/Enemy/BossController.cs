using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossController : MonoBehaviour
{
    Animator anim;
    float moveSpeed = 2.5f;

    float distance = 30;
    float distanceY = 1f;
    float minDistanceX = 2.5f;
    bool move = false;
    private GameObject player;

    public float direction = 1;
    float prevDirection = 1;
    float enemyPlayerDistance;

    public float leftEdge;
    public float rightEdge;

    public Transform firePoint;
    public GameObject bullet;
    public GameObject bigBullet;

    public GameObject virtCamera;
    private bool isShooting = false;
    private bool secondPhase = false;
    public Animator transition;
    public GameObject UI;

    public bool areaLevel = false;
    private bool canAttack = true;
    private float cooldownAttackTime = 0f;
    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }

    void isReadyToMove()
    {
        if (move == true)
        {
            //return;
            move = true;
        }
        else if (Mathf.Abs(player.transform.position.x - transform.position.x) < distance)
        {
            move = true;
        }
    }

    void Move()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < distance && (Mathf.Abs(player.transform.position.x - transform.position.x)) > minDistanceX && Mathf.Abs(player.transform.position.y - transform.position.y) < distanceY)
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < distance)
            {
                prevDirection = direction;
                enemyPlayerDistance = transform.position.x - player.transform.position.x;
                direction = enemyPlayerDistance > 0 ? -1 : 1;
                if (prevDirection != direction)
                {
                    transform.Rotate(0, 180, 0, 0);
                }
            }
        }
        else
        {
            if (transform.position.x <= leftEdge)
            {
                direction = 1;
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else if (transform.position.x >= rightEdge)
            {
                direction = -1;
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            StartCoroutine("WaitForSec");

        }
        if (Math.Abs(player.transform.position.x - transform.position.x) < 10 && Math.Abs(player.transform.position.x - transform.position.x) > 1.9f)
        {
            anim.Play("Rogue_attack_01");
            Shoot();
        }
        else if (Math.Abs(player.transform.position.x - transform.position.x) <= 1.9f && GetComponent<RespawnPlayer>().canAttack)
        {
            anim.Play("Rogue_attack_01");
            GetComponent<RespawnPlayer>().canAttack = false;
            cooldownAttackTime = 1f;
        }
        else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Rogue_attack_01"))
        {
            anim.Play("Rogue_idle_01");
        }

    }

    void Update()
    {
        Move();
        isReadyToMove();

        if (move == true && !secondPhase)
        {
            Vector3 movement = new Vector3(1f, 0f, 0f);
            transform.position += movement * Time.deltaTime * moveSpeed * direction;
        }
        if (GetComponent<EnemyHP>().currentHealth <= 500 && !secondPhase && !areaLevel)
        {
            StartSecondPhase();
            secondPhase = true;
        }
        cooldownAttackEnemy();
    }


    void Shoot()
    {
        if (!isShooting && !anim.GetCurrentAnimatorStateInfo(0).IsName("Rogue_attack_01"))
        {
            isShooting = true;
            Instantiate(bullet, firePoint.position, firePoint.rotation).SetActive(true);
        }
        StartCoroutine("WaitForSec");
        isShooting = false;
    }


    void BigShoot()
    {
        anim.Play("Rogue_attack_01");
        StartCoroutine(WaitForSec());
        Instantiate(bigBullet, firePoint.position, firePoint.rotation).SetActive(true);
        isShooting = false;
    }

    void StartSecondPhase()
    {
        UI.SetActive(false);
        virtCamera.SetActive(false);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        BigShoot();
    }

    void cooldownAttackEnemy()
    {
        if (cooldownAttackTime > 0)
        {
            cooldownAttackTime -= Time.deltaTime;
        }
        if (cooldownAttackTime < 0)
        {
            cooldownAttackTime = 0;
        }
        if (cooldownAttackTime == 0 && !GetComponent<RespawnPlayer>().canAttack)
        {
            GetComponent<RespawnPlayer>().canAttack = true;
            GetComponent<RespawnPlayer>().attacked = false;
        }
        if (cooldownAttackTime > 0 && cooldownAttackTime < 2 && GetComponent<RespawnPlayer>().canAttack)
        {
            GetComponent<RespawnPlayer>().attacked = true;
        }
    }
IEnumerator WaitForSec()
{
    yield return new WaitForSeconds(2);
}
}
