using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetAudioVolume : MonoBehaviour {
    public AudioMixer mixer;
    public Slider slider;

    public string valueName;

    public void SetVolumeLevel() {
        float musicSliderValue = slider.value;
        mixer.SetFloat(valueName, Mathf.Log10(musicSliderValue) * 20); // value in dB
    }

}
