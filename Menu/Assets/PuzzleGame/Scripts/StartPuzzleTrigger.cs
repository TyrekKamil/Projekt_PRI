using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class StartPuzzleTrigger : MonoBehaviour {

    private bool isZoomed = false;
    public float smooth = 0.5f;
    public GameObject virtCamera;
    public Transform zoomingObject;
    void OnTriggerStay2D (Collider2D col) {

        if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ActionButton")))) {
            Statics.recentPlayerPosition = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
            isZoomed = true;
            virtCamera.GetComponent<CinemachineVirtualCamera>().Follow = zoomingObject;
            virtCamera.GetComponent<CinemachineVirtualCamera>().LookAt = zoomingObject;
            virtCamera.SetActive(false);
        }
    }

    void Update () {
        if (isZoomed) {
            Camera.main.orthographicSize = Camera.main.orthographicSize - Time.deltaTime * smooth;
        } 
        if (Camera.main.orthographicSize < 2) {
            SceneManager.LoadScene(4);    
        }
    }
}