﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public MoveWall moveWall;
    float horizontalMove = 0f;

    public float moveSpeed = 20f;

    bool jump = false;
    private int direction = 0;
    public Transform actionPoint;
    public float attackRange = 2.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 50;
    public LayerMask boxLayer;
    public float boxMoveRange = 0.5f;
    void Start()
    {
        if (Statics.sceneWasLeft) {
            gameObject.transform.position = Statics.recentPlayerPosition;
            moveWall.OnMinigameCompletion();
        }
        if (Statics.winPuzzle) {
            Debug.Log(Statics.expAfterPuzzle);
            Statics.winPuzzle = false;
            GetComponent<PlayerUIUpdates>().updateExperience(Statics.expAfterPuzzle);
        }
    }

    void Update()
    {
        if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftButton"))))
        {
            direction = -1;
            MoveObject();
        }
        else if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightButton"))))
        {
            direction = 1;
            MoveObject();
        }

        horizontalMove = direction * moveSpeed;
        

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        direction = 0;

        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpButton"))))
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
        animator.SetTrigger("Attack");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(actionPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<EnemyHP>().TakeDamage(attackDamage);
        }
    }

    void MoveObject() {
        if (Physics2D.OverlapCircleAll(actionPoint.position, boxMoveRange, boxLayer).Length > 0 && !animator.GetBool("IsJumping")) {
            Physics2D.OverlapCircleAll(actionPoint.position, boxMoveRange, boxLayer)[0]
                .GetComponent<BoxMoving>().MoveBox(direction);
            animator.SetTrigger("PushObject");
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("MGround"))
        {
            this.transform.parent = other.transform;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("MGround"))
        {
            this.transform.parent = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (actionPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(actionPoint.position, attackRange);
    }
}
