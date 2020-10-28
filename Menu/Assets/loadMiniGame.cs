using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadMiniGame : MonoBehaviour
{
    public Animator anim;
    public GameObject wall;
    public GameObject virtCamera;
    public Sprite endLever;
    private MoveWall moveWall;
    private bool startCameraMovement = false;
    private void Start()
    {
        if (!Statics.canSwitchLever)
        {
            GetComponent<SpriteRenderer>().sprite = endLever;
        }
        moveWall = wall.GetComponent<MoveWall>();
    }
    private float targetX = 459.69f;
    private float targetY = 5.73f;
    private void Update()
    {
        if (startCameraMovement) {
            if(Camera.main.transform.position.x <= targetX)
                Camera.main.transform.position += new Vector3(0.5f, 0, 0);
            if(Camera.main.transform.position.y >= targetY)
                Camera.main.transform.position += new Vector3(0, -0.1f, 0);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {

        //Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ActionButton")))
        if (Input.GetKeyDown(KeyCode.E) && Statics.canSwitchLever)
        {
            Debug.Log("switched");
            anim.SetBool("switchLever", true);
            //Add this line:
            Statics.recentPlayerPosition = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
            Statics.sceneWasLeft = true;
            Statics.canSwitchLever = false;
            startCameraMovement = true;
            virtCamera.SetActive(false);
            //Camera.main.transform.position = new Vector3(459.69f, 5.73f, Camera.main.transform.position.z);
            StartCoroutine("LoadMinigameScene");
            //Execute at successful minigame completion.

        }


    }
    IEnumerator LoadMinigameScene() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(3);
    }
}
