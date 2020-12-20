using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemyGenerator : MonoBehaviour
{
    private AreaPhase[] phases = {
        new AreaPhase(2, 2), // faza 1
        new AreaPhase(2, 4), // faza 2
        new AreaPhase(3, 8), // faza 3
        new AreaPhase(3, 10), // faza 4
        new AreaPhase(3, 12), // faza 5
        new AreaPhase(3, 14), // faza 6
        new AreaPhase(3, 19), // faza 7
        new AreaPhase(3, 23), // faza 8
        new AreaPhase(3, 29), // faza 9
        new AreaPhase(3, 35), // faza 10
    };

    private int actualPhase = 0;
    private int remainingEnemies = 1;
    private int enemiesInQueue = 0;
    public GameObject[] enemies;
    public GameObject endMenu;
    public GameObject winText;
    public GameObject phaseText;
    public GameObject enemiesCount;
    public GameObject playerUIUpdates;

    void Start()
    {
        startNextPhase();
    }

    void Update()
    {
        if (this.remainingEnemies == 0)
        {
            if (actualPhase == phases.Length)
            {
                winText.SetActive(true);
                phaseText.SetActive(false);
                enemiesCount.SetActive(false);
                endMenu.GetComponent<PauseMenuScript>().Pause();
            }
            else
            {
                if (actualPhase > GLOBAL_DATA.Instance.areaBestScore)
                {
                    GLOBAL_DATA.Instance.areaBestScore = GLOBAL_DATA.Instance.areaBestScore + 1;
                    GameObject.Find("Player").GetComponent<PlayerUIUpdates>().updateExperience(35 * actualPhase);
                }
                startNextPhase();
            }
        }

    }

    private void startNextPhase()
    {
        this.remainingEnemies = this.phases[this.actualPhase].numberOfEnemies;
        this.enemiesInQueue = this.remainingEnemies - this.phases[this.actualPhase].maxEnemiesOnArea;

        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerUIUpdates>().currentHealth = 100;

        int rightSide = this.phases[this.actualPhase].maxEnemiesOnArea / 2;
        int leftSide = this.phases[this.actualPhase].maxEnemiesOnArea - rightSide;

        Debug.Log(leftSide + " " + rightSide);

        for (int i = 0; i < leftSide; i++)
        {
            Instantiate(enemies[0], enemies[0].transform.position + new Vector3(4 * i, 0, 0), enemies[0].transform.rotation).SetActive(true);
        }
        for (int i = 0; i < rightSide; i++)
        {
            Instantiate(enemies[1], enemies[1].transform.position - new Vector3(4 * i, 0, 0), enemies[1].transform.rotation).SetActive(true);
        }
        this.actualPhase += 1;

    }
    public int getActualPhase()
    {
        return this.actualPhase;
    }

    public int getRemainingEnemies()
    {
        return this.remainingEnemies;
    }

    public void decrementRemainingEnemies()
    {
        this.remainingEnemies = this.remainingEnemies - 1;
        if (this.enemiesInQueue > 0)
        {
            this.enemiesInQueue = this.enemiesInQueue - 1;
            int index = this.remainingEnemies % 2;
            Instantiate(enemies[index], enemies[index].transform.position, enemies[index].transform.rotation).SetActive(true);
        }
    }
}
