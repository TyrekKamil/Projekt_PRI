using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    public WaterWaves2D waterLow;
    public WaterWaves2D waterHigh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMinigameCompletion() {
        waterHigh.gameObject.SetActive(true);
        waterLow.gameObject.SetActive(false);
        /*GameObject gameObject = this.gameObject;
        gameObject.transform.position = new Vector3(465.543f, 18.5594f, 0);
        Rigidbody2D gameObjectBody = gameObject.AddComponent<Rigidbody2D>();
        gameObjectBody.mass = 9999999f;
        gameObjectBody.centerOfMass = new Vector2(0f, -2f);
        gameObjectBody.rotation = -16;*/
    }
}
