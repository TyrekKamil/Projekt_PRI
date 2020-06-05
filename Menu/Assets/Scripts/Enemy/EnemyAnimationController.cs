using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAnimationController : MonoBehaviour
{
    Animator anim;
    float moveSpeed = 2.5f;

    float distance = 10;
    bool move = false;
    public GameObject player;
    void Start() {
        anim = GetComponent<Animator>();
    }

    void isReadyToMove() {
        Debug.Log(move);
        if(move == true) {
            return;
        } else if(Mathf.Abs(player.transform.position.x - transform.position.x) < distance) {
            move = true;
        }
    }
    void Update() {
        isReadyToMove();
        if(Input.GetKeyDown("1")) {
            anim.Play("Rogue_death_01");
        } else if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Rogue_death_01") && move == true) {
            Vector3 movement = new Vector3(1f, 0f, 0f);
            transform.position += movement * Time.deltaTime * moveSpeed;
            anim.Play("Rogue_walk_01");
        }
    }
}