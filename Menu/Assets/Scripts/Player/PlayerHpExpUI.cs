using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpExpUI : MonoBehaviour
{
    public Slider sliderHP, sliderEXP;

    public void SetMaxHealth(int hp)
    {
        sliderHP.maxValue = hp;
        sliderHP.value = hp;

    }
    public void SetHealth(int hp)
    {
        sliderHP.value = hp;
    }

    public void SetMaxExp(int exp)
    {
        sliderEXP.maxValue = exp;
    }

    public void SetExperience(int exp)
    {
        sliderEXP.value = exp;
    }
}
