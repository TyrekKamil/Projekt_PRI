using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetAudioVolume : MonoBehaviour {
    public AudioMixer mixer;
    public Slider slider;

    public string valueName;

    void Start(){
        if(PlayerPrefs.GetString(valueName).Length > 0) {
            slider.value = float.Parse(PlayerPrefs.GetString(valueName));
        }
    }
    public void SetVolumeLevel() {
        mixer.SetFloat(valueName, Mathf.Log10(slider.value) * 20); 
        PlayerPrefs.SetString(valueName, slider.value.ToString()); // value in dB
    }

}
