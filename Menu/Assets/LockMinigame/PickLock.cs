using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickLock : MonoBehaviour
{
    private Vector3 currentEulerAngles;
    private Quaternion currentRotation;
    public Transform picklockTransform;
    private int direction = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && direction == 0)
        {
            direction = -1;
        }
        if (Input.GetKeyDown(KeyCode.D) && direction == 0)
        {
            direction = 1;
        }
        if (direction == 1 || direction == -1)
        {
            move();
        }
        if (direction == 2 || direction == -2)
        {
            moveCenter();
        }
    }

    void move()
    {
        Debug.Log("move " + direction);
        picklockTransform.position += new Vector3(100 * direction, 0, 0) * 0.1f;
        if (Mathf.Abs(picklockTransform.localPosition.x) > 500)
        {
            direction = direction == 1 ? -2 : 2;
        }
    }
    void moveCenter()
    {
        Debug.Log(picklockTransform.localPosition.x);
        picklockTransform.position += new Vector3(50 * direction, 0, 0) * 0.1f;
        if ((direction/2) * picklockTransform.localPosition.x > 0)
        {
            direction = 0;
        }
    }
}
