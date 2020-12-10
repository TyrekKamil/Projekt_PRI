using UnityEngine;
using UnityEngine.UI;

public class EnemiesRemainingCounter : MonoBehaviour
{
    public GameObject areaEnemyGenerator;
    void Update()
    {
        gameObject.GetComponent<Text>().text = areaEnemyGenerator.GetComponent<AreaEnemyGenerator>().getRemainingEnemies().ToString();
    }
}