using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public string soundType;

    public AudioMixer mixer;

    void Start(){
        if(PlayerPrefs.GetString(soundType).Length > 0) {
            mixer.SetFloat(soundType, Mathf.Log10(float.Parse(PlayerPrefs.GetString(soundType))) * 20);
        }
        Debug.Log(soundType + " " + PlayerPrefs.GetString(soundType)); 
    }
}
