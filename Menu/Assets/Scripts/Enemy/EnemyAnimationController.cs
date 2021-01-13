using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyAnimationController : MonoBehaviour
{
    Animator anim;
    float moveSpeed = 2.5f;

    float distance = 30;
    float distanceY = 1f;
    float minDistanceX = 1.5f;

    private bool move = false;
    private GameObject player;

    public float direction = 1;
    float prevDirection = 1;
    float enemyPlayerDistance;

    public float leftEdge;
    public float rightEdge;
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

    void reverse()
    {
        if (player.transform.position.x <= rightEdge && player.transform.position.x >= leftEdge)
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < distance && (Mathf.Abs(player.transform.position.x - transform.position.x)) > minDistanceX && Mathf.Abs(player.transform.position.y - transform.position.y) < distanceY)
            {
                prevDirection = direction;
                enemyPlayerDistance = transform.position.x - player.transform.position.x;
                direction = enemyPlayerDistance > 0 ? -1 : 1;
                if (prevDirection != direction /* && direction != 0 && Math.Abs(enemyPlayerDistance) > 0.1 */)
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
        if (Math.Abs(player.transform.position.x - transform.position.x) < 2.5 && canAttack)
        {
            anim.Play("Rogue_attack_01");
            canAttack = false;
            cooldownAttackTime = 2f;
        }
        else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Rogue_attack_01"))
        {
            anim.Play("Rogue_idle_01");
        }



    }
    void Update()
    {
        reverse();
        isReadyToMove();
        cooldownAttackEnemy();
        if (move == true)
        {
            Vector3 movement = new Vector3(1f, 0f, 0f);
            transform.position += movement * Time.deltaTime * moveSpeed * direction;
        }
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
        if (cooldownAttackTime == 0 && !canAttack)
        {
            canAttack = true;
        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
    }
}