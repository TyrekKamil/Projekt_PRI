using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    float horizontalMove = 0f;

    public float moveSpeed = 20f;

    bool jump = false;

    private int direction = 0;
    public Transform attackPoint;
    public float attackRange = 2.5f;
    public LayerMask enemyLayers;
    private bool isDamaged = false;
    public int attackDamage = 50;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftButton"))))
        {
            direction = -1;
        }
        else if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightButton"))))
        {
            direction = 1;
        }

        horizontalMove = direction * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        direction = 0;

        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpButton"))) && isDamaged)
        {
            animator.SetBool("IsJumping", true);
            jump = true;
        }
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AttackButton"))))
        {
            Attack();
        }
    }
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;

    }

    void Attack()
    {
        Debug.Log("Attack");
        //Animacja ataku TODO
        animator.SetTrigger("Attack");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<EnemyHP>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void JumpBlock() {
        this.isDamaged = true;
        StartCoroutine(WaitForSec());
        this.isDamaged = false;
    }

        IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
    }
    
}
