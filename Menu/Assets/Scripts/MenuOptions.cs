using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class MenuOptions : MonoBehaviour
{

    public Dropdown resolutionDropdown;

    public Toggle fullScreen;

    public Toggle vSync;
    Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        bool fs = true;
        bool vS = true;

        if(PlayerPrefs.GetString("FullScreen").Length == 1) {
            fs = PlayerPrefs.GetString("FullScreen") == "1" ? true : false;
        } 
        if(PlayerPrefs.GetString("VSync").Length == 1) {
            vS = PlayerPrefs.GetString("VSync") == "1" ? true : false;
        }
        if(PlayerPrefs.GetString("Resolution").Contains("x")) {
            Screen.SetResolution(int.Parse(PlayerPrefs.GetString("Resolution").Split('x')[0]), 
            int.Parse(PlayerPrefs.GetString("Resolution").Split('x')[1]), 
            fs);
        }
        List<string> options = new List<string>();

        int currentRes = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentRes = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentRes;
        resolutionDropdown.RefreshShownValue();
        fullScreen.isOn = fs;
        vSync.isOn = vS;
        QualitySettings.vSyncCount = vS == true ? 1 : 0;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetString("Resolution", resolution.width.ToString() + "x" + resolution.height.ToString());
    }
    public void SetfullScreen(bool isFS)
    {
        Screen.fullScreen = isFS;
        PlayerPrefs.SetString("FullScreen", isFS == true ? "1" : "0");
    }

    public void SetVSync(bool isVSync) {
        QualitySettings.vSyncCount = isVSync == true ? 1 : 0;
        PlayerPrefs.SetString("VSync", isVSync == true ? "1" : "0");
    }
}
