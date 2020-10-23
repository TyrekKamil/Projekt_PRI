using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBackgroundOnStart : MonoBehaviour
{
    public GameObject buttons;
    private bool gameStarted;
    void Start() {
        transform.position += new Vector3(0, 0, -6);

    }
    void Update() {
        if (transform.position.x < 9 && !gameStarted) {
            transform.position += new Vector3(2f, 0, 0) * Time.deltaTime;
        } else if (!gameStarted) {
            buttons.SetActive(true);
            gameStarted = true;
        }
    }
}
