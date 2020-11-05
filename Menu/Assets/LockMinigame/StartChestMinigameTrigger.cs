using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartChestMinigameTrigger : MonoBehaviour
{
    public GameObject lightBox;
    private bool isZoomed = false;
    public GameObject virtCamera;
    public Transform zoomingObject;

    void Start()
    {
        if (Statics.endChest)
        {
            lightBox.SetActive(false);
        }
    }

    void Update()
    {
        if (isZoomed)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize - Time.deltaTime * 2;
        }
        if (Camera.main.orthographicSize < 1.5)
        {
            SceneManager.LoadScene("Lock");
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {

        if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ActionButton"))) && !Statics.endChest)
        {
            Statics.recentPlayerPosition = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
            Statics.lastSceneId = SceneManager.GetActiveScene().name;
            Statics.sceneWasLeft = true;
            isZoomed = true;
            virtCamera.SetActive(false);
            Camera.main.transform.position = new Vector3(zoomingObject.position.x, zoomingObject.position.y, Camera.main.transform.position.z);
        }
    }
}
