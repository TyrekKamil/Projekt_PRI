using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwing : MonoBehaviour
{
    private bool isTouching = false;
    private Collision2D col;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("I've entered collider");
        isTouching = true;
        col = collision;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("I've quit collider");
        isTouching = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTouching)
        {
            Debug.Log("I'm getting parent");
            col.transform.parent = this.transform;
            isTouching = false;
        }
        if (Input.GetKeyUp(KeyCode.E) && col != null)
        {
            col.transform.parent = null;
        }
    }
}
