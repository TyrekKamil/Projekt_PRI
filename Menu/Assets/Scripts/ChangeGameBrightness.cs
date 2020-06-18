using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ChangeGameBrightness : MonoBehaviour
{
    public Slider brightness_Slider;

    public static float intensityValue = 0.12f;
    void Start()
    {
        intensityValue = brightness_Slider.value;
    }
    public void SetLightIntensity()
    {
        intensityValue = brightness_Slider.value;
    }

}
