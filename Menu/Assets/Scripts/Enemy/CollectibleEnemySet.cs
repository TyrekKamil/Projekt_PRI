using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleEnemySet : MonoBehaviour
{
    public HashSet<string> killedEnemies { get; private set; } = new HashSet<string>();

    private void Awake()
    {
        GameEvents.SaveInitiated += Save;
        Load();
    }
    public void Save()
    {
        SaveLoad.Save(killedEnemies, "Killed enemies");
    }

    private void Load()
    {
        if(SaveLoad.SaveExists("Killed enemies"))
        {
            killedEnemies = SaveLoad.Load<HashSet<string>>("Killed enemies");
        }
    }
}
