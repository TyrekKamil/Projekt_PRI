using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMoving : MonoBehaviour
{
    private Vector3 movement = new Vector3(1f, 0f, 0f);
    private float moveSpeed = 2.5f;

    public void MoveBox(int boxDirect) { 
        Debug.Log("Move to " + boxDirect);
        transform.position += movement * Time.deltaTime * moveSpeed * boxDirect;    
    }
}
