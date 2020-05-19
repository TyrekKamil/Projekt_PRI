using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    Animator anim;
    float moveSpeed = 2.5f;


    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        if(Input.GetKeyDown("1")) {
            anim.Play("Rogue_death_01");
        } else if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Rogue_death_01")) {
            Vector3 movement = new Vector3(1f, 0f, 0f);
            transform.position += movement * Time.deltaTime * moveSpeed;
        }
    }
}