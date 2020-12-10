using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemyGenerator : MonoBehaviour
{
    private AreaPhase[] phases = {
        new AreaPhase(2, 2), // faza 1
        new AreaPhase(2, 4), // faza 2
        new AreaPhase(3, 8), // faza 3
    };

    private int actualPhase = 0;
    private int remainingEnemies = 1;
    public GameObject[] enemies;

    void Start()
    {
        startNextPhase();
    }

    void Update()
    {  ///text
        if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftButton"))))
        {
            this.remainingEnemies -= 1;
        }

        if (this.remainingEnemies == 0)
        {
            startNextPhase();
        }
    }

    private void startNextPhase()
    {
        this.remainingEnemies = this.phases[this.actualPhase].numberOfEnemies;
        this.actualPhase += 1;

        int leftSide = this.phases[this.actualPhase].maxEnemiesOnArea / 2;
        int rightSide = this.phases[this.actualPhase].maxEnemiesOnArea - leftSide;

        for (int i = 0; i < leftSide; i++)
        {
            Instantiate(enemies[0], enemies[0].transform.position + new Vector3(4 * i, 0, 0), enemies[0].transform.rotation).SetActive(true);
        }
        for (int i = 0; i < rightSide; i++)
        {
            Instantiate(enemies[1], enemies[1].transform.position - new Vector3(4 * i, 0, 0), enemies[1].transform.rotation).SetActive(true);
        }
    }
    public int getActualPhase()
    {
        return this.actualPhase;
    }

    public int getRemainingEnemies()
    {
        return this.remainingEnemies;
    }
}
