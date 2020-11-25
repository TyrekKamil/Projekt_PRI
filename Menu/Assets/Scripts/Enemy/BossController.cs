﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossController : MonoBehaviour
{
    Animator anim;
    float moveSpeed = 2.5f;

    float distance = 30;   
    bool move = false;
    public GameObject player;

    float direction = 1;
    float prevDirection = 1;
    float enemyPlayerDistance;

    public float leftEdge;
    public float rightEdge;
        
    public Transform firePoint;
    public GameObject bullet;
    private bool isShooting = false;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void isReadyToMove() {
        if(move == true) {
            //return;
            move = true;
        } else if(Mathf.Abs(player.transform.position.x - transform.position.x) < distance) {
            move = true;
        }
    }

    void Move() {
        if (player.transform.position.x <= rightEdge && player.transform.position.x >= leftEdge) {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < distance) { 
                prevDirection = direction;
                enemyPlayerDistance = transform.position.x - player.transform.position.x;
                direction = enemyPlayerDistance > 0 ? -1 : 1;
                if (prevDirection != direction) { 
                    transform.Rotate(0, 180, 0, 0); 
                }
            }
        } else {
            if (transform.position.x <= leftEdge || transform.position.x >= rightEdge) {
                direction *= -1;
                transform.Rotate(0, 180, 0, 0);
            }
            StartCoroutine("WaitForSec");

        } if (Math.Abs(player.transform.position.x - transform.position.x) < 10 && Math.Abs(player.transform.position.x - transform.position.x) > 2.5) {
            anim.Play("Rogue_attack_01");
            Shoot();
        } else if (Math.Abs(player.transform.position.x - transform.position.x) < 2.5) {
            anim.Play("Rogue_attack_01");
        }  else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Rogue_attack_01")) {
            anim.Play("Rogue_idle_01");
        }

    }

    void Update() {
        Move();
        isReadyToMove();
    
        if(move == true) {
            Vector3 movement = new Vector3(1f, 0f, 0f);
            transform.position += movement * Time.deltaTime * moveSpeed * direction;    
        }
    }


    void Shoot() {
        if(!isShooting && !anim.GetCurrentAnimatorStateInfo(0).IsName("Rogue_attack_01")) {
            isShooting = true;
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
        StartCoroutine("WaitForSec");
        isShooting = false;
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
    }
}