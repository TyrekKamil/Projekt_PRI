using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadMiniGame : MonoBehaviour
{
    public Animator anim;
    public GameObject wall;
    public Sprite endLever;
    private MoveWall moveWall;
    
    private void Start()
    {
        if (!Statics.canSwitchLever)
        {
            GetComponent<SpriteRenderer>().sprite = endLever;
        }
        moveWall = wall.GetComponent<MoveWall>();
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
            StartCoroutine("LoadMinigameScene");
            //Execute at successful minigame completion.

        }


    }
    IEnumerator LoadMinigameScene() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(3);
    }
}
