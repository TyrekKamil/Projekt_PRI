using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class StartPuzzleTrigger : MonoBehaviour
{

    private bool isZoomed = false;
    public float smooth = 0.5f;
    public GameObject virtCamera;
    public Transform zoomingObject;
    public GameObject lightPuzzle;
    void OnTriggerStay2D(Collider2D col)
    {

        if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ActionButton"))) && !Statics.winPuzzle)
        {
            Statics.recentPlayerPosition = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
            Statics.lastSceneId = SceneManager.GetActiveScene().name;
            isZoomed = true;
            virtCamera.SetActive(false);
            Camera.main.transform.position = new Vector3(zoomingObject.position.x, zoomingObject.position.y, Camera.main.transform.position.z);
        }
    }

    void Start()
    {
        if (Statics.winPuzzle)
        {
            lightPuzzle.SetActive(false);
        }
    }

    void Update()
    {
        if (isZoomed)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize - Time.deltaTime * smooth;
        }
        if (isZoomed && Camera.main.orthographicSize < 0.5)
        {
            isZoomed = false;
            SceneManager.LoadScene("Puzzle");
        }
    }
}