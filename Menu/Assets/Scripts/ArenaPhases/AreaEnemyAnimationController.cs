using System.Collections;
using UnityEngine;
using System;

public class AreaEnemyAnimationController : MonoBehaviour
{
    Animator anim;
    float moveSpeed = 2.5f;

    bool move = false;
    public GameObject player;

    public float direction = 1;
    float prevDirection = 1;

    float distanceY = 1f;
    float minDistanceX = 1.5f;
    public LayerMask enemyLayer;

    public int side = -1;
    private float cooldownAttackTime = 0f;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void isReadyToMove()
    {
        if (move == true)
        {
            move = true;
        }

        else if (side * (transform.position.x - Physics2D.OverlapCircleAll(transform.position, 0.5f, enemyLayer)[0].transform.position.x) < 2)
        {

            move = true;
        }
    }

    void Move()
    {
        if ((Mathf.Abs(player.transform.position.x - transform.position.x)) > minDistanceX && Mathf.Abs(player.transform.position.y - transform.position.y) < distanceY)
        {
            prevDirection = direction;
            float enemyPlayerDistance = transform.position.x - player.transform.position.x;
            direction = enemyPlayerDistance > 0 ? -1 : 1;
            if (prevDirection != direction /* && direction != 0 && Math.Abs(enemyPlayerDistance) > 0.1 */)
            {
                transform.Rotate(0, 180, 0, 0);
            }
        }
        else if (Math.Abs(player.transform.position.x - transform.position.x) < 2.5 && GetComponent<RespawnPlayer>().canAttack)
        {
            anim.Play("Rogue_attack_01");
            GetComponent<RespawnPlayer>().canAttack = false;
            cooldownAttackTime = 2f;
        }
        else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Rogue_attack_01"))
        {
            anim.Play("Rogue_idle_01");
        }
    }


    void Update()
    {
        isReadyToMove();
        Move();
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