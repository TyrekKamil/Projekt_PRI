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
    void Start() {
         
    }

    void Update()
    {
        if(Input.GetKey((KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftButton")))) {
            direction = -1;
        } 
        else if(Input.GetKey((KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightButton")))) {
            direction = 1;
        }

        horizontalMove = direction * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        direction = 0;
        
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpButton")))) {
            animator.SetBool("IsJumping", true);
            jump = true;
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
}
