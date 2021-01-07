using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{

    public static bool gameIsPaused = false;
    private GameObject mouse;
    public bool mouseOnScene = false;
    void Start() { 
        mouse = GameObject.Find("normalCursor");
        mouseOnScene = mouse.GetComponent<MouseCursor>().mouseOnScene; 
    }
    public GameObject pauseMenu;
    void Update() {
        if (Input.GetKeyDown("escape")) {
            if(gameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }    
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        mouse.GetComponent<MouseCursor>().SetVisibleCursor(mouseOnScene);
    } 
    
    public void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        mouse.GetComponent<MouseCursor>().SetVisibleCursor(true);
    } 
    public void Exit() {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void SaveGame()
    {
        if (SaveLoad.SaveExists("PlayerStats"))
        {
            SaveLoad.SeriouslyDeleteAllSaveFiles();
        }

        GameEvents.OnSaveSceneInitiated();
        GameEvents.OnSaveInitiated();
        Resume();
    }

    public void LoadGame()
    {
        GameEvents.OnLoadSceneInitiated();
        Resume();
    }
}
