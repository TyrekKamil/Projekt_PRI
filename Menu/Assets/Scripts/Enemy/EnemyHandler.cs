using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public void OnSave()
    {
        SerializationManager.Save("enemies", SaveData.current);
    }

    public void OnLoad()
    {
        GameEvents.current.LoadEvent();

        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/enemies.save");

        for(int i=0; i < SaveData.current.enemyData.Count; i++)
        {
            EnemyData currentEnemy = SaveData.current.enemyData[i];
            GameObject obj = Instantiate(enemies[int.Parse(currentEnemy.id)]);
            EnemyHP enemy = obj.GetComponent<EnemyHP>();
            enemy.enemyData = currentEnemy;
            enemy.transform.position = currentEnemy.position;
            enemy.transform.rotation = currentEnemy.rotation;
        }
    }
    
}
