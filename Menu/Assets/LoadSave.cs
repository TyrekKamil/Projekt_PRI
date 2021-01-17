using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSave : MonoBehaviour
{

    public void Run()
    {
        if (SaveLoad.SaveExists("Scene"))
        {
            SeriazableScene serializedScene = SaveLoad.Load<SeriazableScene>("Scene");
            int savedScene = serializedScene.currentScene;
            SceneManager.LoadScene(savedScene);
            Statics.isLoadedGame = true;


        }
    }

    
}
