using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPUI : MonoBehaviour
{
    public Slider sliderHP;

    void Start()
    {
        sliderHP.value = 100;
        sliderHP.maxValue = 100;
    }

    public void TakeDamageUI(int hp)
    {
        sliderHP.value = sliderHP.value - hp;
    }

}
