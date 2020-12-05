using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private float timePassed = 0;
    public GameObject[] enemies;

    void Start()
    {
        InvokeRepeating("GenerateEnemy", 2.0f, 15f);
    }

    void GenerateEnemy()
    {
        int random = new System.Random().Next(1, 3);
        if (!GameObject.Find("Enemy" + random))
        {
            GameObject enemy = Instantiate(enemies[random], enemies[random].transform.position, enemies[random].transform.rotation);
            enemy.SetActive(true);
            enemy.name = "Enemy" + random;
        } else {
            GenerateEnemy();
        }

    }
}
