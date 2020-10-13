using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadMiniGame : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {

    }
    void OnTriggerStay2D(Collider2D col)
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("switched");
            anim.SetBool("switchLever", true);
            StartCoroutine("LoadMinigameScene");
            
        }

    }
    IEnumerator LoadMinigameScene() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(3);
    }
}
