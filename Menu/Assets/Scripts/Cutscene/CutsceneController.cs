using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public GameObject[] subtitles;
    public Animator teleportAnim;
    void Start()
    {
        teleportAnim.SetBool("TeleportCutscene", false);
        GLOBAL_DATA.Instance.HP = 100;
        GLOBAL_DATA.Instance.Level = 1;
        GLOBAL_DATA.Instance.SkillPoints = 0;
        GLOBAL_DATA.Instance.XP = 0;
        GLOBAL_DATA.Instance.areaBestScore = 0;
        GLOBAL_DATA.Instance.chestOpened = new bool[3];
        GLOBAL_DATA.Instance.unlockedSkillTypeList = new List<PlayerSkills.SkillType>();
        Statics.canSwitchLever = true;
        Statics.sceneWasLeft = false;
        Statics.playPuzzle = false;
        Statics.isOnRope = false;
        Statics.lastSceneId = "";
        Statics.endChest = false;
        Statics.chestOpened = false;
        Statics.itemDropped = false;
        Statics.isHpBoostedFromSkill = false;
        Statics.isImmortal = false;
        Statics.isLoadedGame = false;
        Statics.winPuzzle = false;
        Statics.puzzle = new Dictionary<string, bool>()
        {
            { "Puzzle",     false },
            { "Puzzle-1-1", false },
            { "Puzzle-1-2", false }
        };
        SecondScene();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            LoadScene();
        }
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
        StartCoroutine(DelayLoadLevelTutorial());
    }
    IEnumerator DelayLoadLevelTutorial()
    {
        yield return new WaitForSeconds(5);
        LoadScene();
    }
    void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
