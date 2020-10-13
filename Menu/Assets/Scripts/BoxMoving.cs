using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMoving : MonoBehaviour
{   
    private Vector3 movement = new Vector3(1f, 0f, 0f);
    public float moveSpeed = 1.5f;
    public LayerMask wallLayer;
    public LayerMask boxesLayer;
    public void MoveBox(int boxDirect) { 
        Collider2D[] walls = Physics2D.OverlapCircleAll(transform.position, 0.5f, wallLayer);
        Collider2D[] boxes = Physics2D.OverlapCircleAll(transform.position, 1.9f, boxesLayer);
        if(walls.Length == 0 && boxes.Length == 1) {
            transform.position += movement * Time.deltaTime * moveSpeed * boxDirect;
        }
    }
}