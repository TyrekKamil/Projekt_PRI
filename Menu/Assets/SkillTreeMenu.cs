using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeMenu : MonoBehaviour
{
    private GameObject mouse;
    public bool mouseOnScene = false;
    private bool isSkillTreeOpen = false;
    void Start()
    {
        mouse = GameObject.Find("normalCursor");
        mouseOnScene = mouse.GetComponent<MouseCursor>().mouseOnScene;
    }
    [SerializeField]
    private GameObject skillTreeMenu;
    void Update()
    {
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SkillTreeButton")))) {
            if (isSkillTreeOpen)
            {
                Resume();
            }
            else {
                Pause();
            }
        }
        if (Input.GetKeyDown("escape"))
        {
            if (isSkillTreeOpen)
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        skillTreeMenu.SetActive(false);
        Time.timeScale = 1f;
        isSkillTreeOpen = false;
        mouse.GetComponent<MouseCursor>().SetVisibleCursor(mouseOnScene);
    }

    public void Pause()
    {
        skillTreeMenu.SetActive(true);
        Time.timeScale = 0f;
        isSkillTreeOpen = true;
        mouse.GetComponent<MouseCursor>().SetVisibleCursor(true);
    }
}
