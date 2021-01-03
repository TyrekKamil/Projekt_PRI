using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject[] subtitles;
    public Animator teleportAnim;
    void Start()
    {
        teleportAnim.SetBool("TeleportCutscene", false);
        SecondScene();
    }

    void Update()
    {
    }

    public void SecondScene()
    {
        StartCoroutine(SecondSceneDelay());
    }

    IEnumerator SecondSceneDelay()
    {
        yield return new WaitForSeconds(5);
        Camera.main.transform.position = new Vector3(-8.74f, 4.35f, -14f);
        subtitles[0].SetActive(false);
        subtitles[1].SetActive(true);
        StartCoroutine(ThirdSceneDelay());
    }

        IEnumerator ThirdSceneDelay()
    {
        yield return new WaitForSeconds(5);
        teleportAnim.SetBool("TeleportCutscene", true);
        Camera.main.transform.position = new Vector3(-8.74f, 4.35f, -14f);
        subtitles[1].SetActive(false);
        subtitles[2].SetActive(true);
    }
}
