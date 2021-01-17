using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveCurrentScene : MonoBehaviour
{
    private int _currentScene;

    private void Start()
    {
        GameEvents.SaveSceneInitiated += SaveScene;
        GameEvents.LoadSceneInitiated += LoadScene;
        _currentScene = SceneManager.GetActiveScene().buildIndex;

    }

    private void SaveScene()
    {
        SeriazableScene serializedScene = new SeriazableScene()
        {
            currentScene = _currentScene
        };

        SaveLoad.Save<SeriazableScene>(serializedScene, "Scene");
    }

    private void LoadScene()
    {
        if (SaveLoad.SaveExists("Scene"))
        {
            SeriazableScene serializedScene = SaveLoad.Load<SeriazableScene>("Scene");
            int savedScene = serializedScene.currentScene;

            SceneManager.LoadSceneAsync(savedScene);
            Statics.isLoadedGame = true;
          
            
        }
    }

    private void OnDestroy()
    {
        GameEvents.SaveSceneInitiated -= SaveScene;
        GameEvents.LoadSceneInitiated -= LoadScene;
    }
}

[System.Serializable]
public class SeriazableScene
{
    public int currentScene { get; set; }
}

