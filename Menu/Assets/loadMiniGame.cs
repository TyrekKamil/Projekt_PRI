using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadMiniGame : MonoBehaviour
{
    public Animator anim;
    public GameObject wall;
    private MoveWall moveWall;
    private void Start()
    {
        moveWall = wall.GetComponent<MoveWall>();
    }
    void OnTriggerStay2D(Collider2D col)
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("switched");
            anim.SetBool("switchLever", true);
            //Add this line:
            //StartCoroutine("LoadMinigameScene");
            //Execute at successful minigame completion.
            moveWall.OnMinigameCompletion();
        }

    }
    IEnumerator LoadMinigameScene() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(3);
    }
}
