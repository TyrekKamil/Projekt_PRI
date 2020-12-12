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
    public LayerMask enemyLayer;

    public int side = -1;
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

        else if ( side * (transform.position.x - Physics2D.OverlapCircleAll(transform.position, 0.5f, enemyLayer)[0].transform.position.x) < 2)
        {
            
            move = true;
        }
    }

    void Move()
    {
        prevDirection = direction;
        float enemyPlayerDistance = transform.position.x - player.transform.position.x;
        direction = enemyPlayerDistance > 0 ? -1 : 1;
        if (prevDirection != direction)
        {
            transform.Rotate(0, 180, 0, 0);
        }
        else if (Math.Abs(player.transform.position.x - transform.position.x) < 2.5)
        {
            anim.Play("Rogue_attack_01");
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

        if (move == true)
        {
            Vector3 movement = new Vector3(1f, 0f, 0f);
            transform.position += movement * Time.deltaTime * moveSpeed * direction;
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
    }
}