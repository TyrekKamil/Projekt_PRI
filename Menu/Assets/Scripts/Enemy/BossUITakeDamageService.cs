using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUITakeDamageService : MonoBehaviour
{
    public GameObject bossUI;

    public void TakeDamageUI(int damage) {
        bossUI.GetComponent<BossHPUI>().TakeDamageUI(damage);
    }
}
