using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

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
        direction = 0;
        
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpButton")))) {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
