using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{

    public static bool gameIsPaused = false;

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
    } 
    
    void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    } 
    public void Exit() {
        Resume();
        SceneManager.LoadScene(0);
    }
}