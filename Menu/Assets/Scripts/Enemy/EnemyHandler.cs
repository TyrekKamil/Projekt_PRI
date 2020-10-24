using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public List<SerializableEnemy> killedEnemies { get; set; } = new List<SerializableEnemy>();

    public void AddToList(SerializableEnemy enemy)
    {
        killedEnemies.Add(enemy);
    }

    public void AddEnemies(List<SerializableEnemy> enemies)
    {
        foreach(SerializableEnemy enemy in killedEnemies)
        {
            AddToList(enemy);
        }
    }

    public void RemoveFromList(SerializableEnemy enemy)
    {
        killedEnemies.Remove(enemy);
    }

    public void Showlist()
    {
        Debug.Log(killedEnemies.Capacity);
    }

   /* public List<GameObject> enemies = new List<GameObject>();
    public void OnSave()
    {
        SerializationManager.Save("enemies", SaveData.current.enemyData);
    }

    public void OnLoad()
    {
        GameEvents.LoadEvent();

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
 */ 
   

}
