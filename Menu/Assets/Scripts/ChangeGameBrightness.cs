using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ChangeGameBrightness : MonoBehaviour
{
    public Slider brightness_Slider;

    public static float intensityValue = 0.12f;
    void Start() {
        if(PlayerPrefs.GetString("Brightness").Length >= 1) {
            intensityValue = float.Parse(PlayerPrefs.GetString("Brightness"));
        }
        brightness_Slider.value = intensityValue;
    }
    public void SetLightIntensity() {
        intensityValue = brightness_Slider.value;
        PlayerPrefs.SetString("Brightness", intensityValue.ToString());
    }

}
