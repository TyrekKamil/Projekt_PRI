using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindScript : MonoBehaviour {

    private Dictionary<string, KeyCode> keysConfig = new Dictionary<string, KeyCode>();
    public Text left, right, jump, attack, action;
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
        if(PlayerPrefs.GetString("AttackButton").Length > 0) {
            keysConfig.Add("AttackButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AttackButton", "LeftControl")));
        } else {
            keysConfig.Add("AttackButton", KeyCode.LeftControl);
            PlayerPrefs.SetString("AttackButton", KeyCode.LeftControl.ToString());
        }
        if(PlayerPrefs.GetString("ActionButton").Length > 0) {
            keysConfig.Add("ActionButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ActionButton", "E")));
        } else {
            keysConfig.Add("ActionButton", KeyCode.E);
            PlayerPrefs.SetString("ActionButton", KeyCode.E.ToString());
        }
        left.text = keysConfig["LeftButton"].ToString();
        right.text = keysConfig["RightButton"].ToString();
        jump.text = keysConfig["JumpButton"].ToString();
        attack.text = keysConfig["AttackButton"].ToString();
        action.text = keysConfig["ActionButton"].ToString();
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
