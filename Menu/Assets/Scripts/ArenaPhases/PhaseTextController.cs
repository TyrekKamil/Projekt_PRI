using UnityEngine;
using UnityEngine.UI;

public class PhaseTextController : MonoBehaviour
{
    public GameObject areaEnemyGenerator;
    void Update()
    {
        gameObject.GetComponent<Text>().text = areaEnemyGenerator.GetComponent<AreaEnemyGenerator>().getActualPhase().ToString();
    }
}
