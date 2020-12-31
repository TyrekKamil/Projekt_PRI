using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveCurrentScene : MonoBehaviour
{
    private string _currentScene;

    private void Start()
    {
        GameEvents.SaveSceneInitiated += SaveScene;
        GameEvents.LoadSceneInitiated += LoadScene;
        _currentScene = SceneManager.GetActiveScene().name;

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
            string savedScene = serializedScene.currentScene;

            //TODO: check the current map and loadSave instead of LoadScene if scene is the same
            SceneManager.LoadScene(savedScene);
            Statics.isLoadedGame = true;
          
            
        }
    }
}

[System.Serializable]
public class SeriazableScene
{
    public string currentScene { get; set; }
}

