using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMinigameCompletion() {
        GameObject gameObject = this.gameObject;
        Rigidbody2D gameObjectBody = gameObject.AddComponent<Rigidbody2D>();
        gameObjectBody.mass = 25f;
        gameObjectBody.centerOfMass = new Vector2(0f, -2f);
        gameObjectBody.rotation = -16;
    }
}
