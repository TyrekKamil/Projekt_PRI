using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindScript : MonoBehaviour {

    private Dictionary<string, KeyCode> keysConfig = new Dictionary<string, KeyCode>();
    public Text left, right, jump;
    private GameObject currentKey;

    private Color32 normal = new Color32(0, 0, 0, 128);

    private Color32 selected = new Color32(0, 0, 0, 180);
        void Start() {
        Debug.Log(PlayerPrefs.GetString("LeftButton").Length > 0 );
        if(PlayerPrefs.GetString("LeftButton").Length > 0) {
            keysConfig.Add("LeftButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftButton", "Left")));
        } else {
            keysConfig.Add("LeftButton", KeyCode.A);
            PlayerPrefs.SetString("LeftButton", KeyCode.A.ToString());
        } 
        if(PlayerPrefs.GetString("RightButton").Length > 0) {
            keysConfig.Add("RightButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightButton", "Right")));
        } else {
            keysConfig.Add("RightButton", KeyCode.D);
            PlayerPrefs.SetString("RightButton", KeyCode.D.ToString());
        } 
        if(PlayerPrefs.GetString("JumpButton").Length > 0) {
            keysConfig.Add("JumpButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpButton", "Space")));
        } else {
            keysConfig.Add("JumpButton", KeyCode.Space);
            PlayerPrefs.SetString("JumpButton", KeyCode.Space.ToString());
        }

        left.text = keysConfig["LeftButton"].ToString();
        right.text = keysConfig["RightButton"].ToString();
        jump.text = keysConfig["JumpButton"].ToString();
    }

    void OnGUI() {
        if(currentKey != null) {
            Event e = Event.current;
            if(e.isKey) {
                if(!keysConfig.ContainsValue(e.keyCode)) {
                    keysConfig[currentKey.name] = e.keyCode;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                }
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        saveKeys();
        }
    }
    public void changeKey(GameObject clicked) {
        if(currentKey != null) {
            currentKey.GetComponent<Image>().color = normal;
        } 
        currentKey = clicked; 
        currentKey.GetComponent<Image>().color = selected;
    }

    public void saveKeys() {
        foreach (var key in keysConfig) {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }
}
