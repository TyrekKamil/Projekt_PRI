using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 pos1;
    public Vector3 pos2;
    public float speed = 1.0f;
    Vector3 pos;
    public float velocityX;
    private void Awake()
    {
        pos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        velocityX = (transform.position.x - pos.x) / Time.deltaTime;
        pos = transform.position;
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f)); 
    }
}
