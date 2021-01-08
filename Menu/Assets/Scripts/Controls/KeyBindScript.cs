using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindScript : MonoBehaviour
{

    private Dictionary<string, KeyCode> keysConfig = new Dictionary<string, KeyCode>();
    public Text left, right, jump, attack, action, skillTree, bullet, explode, increaseDMG, regenerate, immorality, sprint, dash, invetory;
    private GameObject currentKey;

    private Color32 normal = new Color32(0, 0, 0, 128);

    private Color32 selected = new Color32(0, 0, 0, 180);
    void Start()
    {
        addToKeysConfig("LeftButton", "A");
        addToKeysConfig("RightButton", "D");
        addToKeysConfig("JumpButton", "Space");
        addToKeysConfig("AttackButton", "Ctrl");
        addToKeysConfig("ActionButton", "E");
        addToKeysConfig("SkillTreeButton", "P");
        addToKeysConfig("IncreaseDMGButton", "Alpha1");
        addToKeysConfig("BulletSkillButton", "Alpha2");
        addToKeysConfig("ExplodeSkillButton", "Alpha3");
        addToKeysConfig("RegenHPButton", "Alpha4");
        addToKeysConfig("ImmoralitySkillButton", "Alpha5");
        addToKeysConfig("SprintButton", "B");
        addToKeysConfig("DashButton", "Z");
        addToKeysConfig("DisplayInvetoryButton", "I");

        left.text = keysConfig["LeftButton"].ToString().Replace("Alpha", "");
        right.text = keysConfig["RightButton"].ToString().Replace("Alpha", "");
        jump.text = keysConfig["JumpButton"].ToString().Replace("Alpha", "");
        attack.text = keysConfig["AttackButton"].ToString().Replace("Alpha", "");
        action.text = keysConfig["ActionButton"].ToString().Replace("Alpha", "");
        skillTree.text = keysConfig["SkillTreeButton"].ToString().Replace("Alpha", "");
        increaseDMG.text = keysConfig["IncreaseDMGButton"].ToString().Replace("Alpha", "");
        bullet.text = keysConfig["BulletSkillButton"].ToString().Replace("Alpha", "");
        explode.text = keysConfig["ExplodeSkillButton"].ToString().Replace("Alpha", "");
        regenerate.text = keysConfig["RegenHPButton"].ToString().Replace("Alpha", "");
        immorality.text = keysConfig["ImmoralitySkillButton"].ToString().Replace("Alpha", "");
        sprint.text = keysConfig["SprintButton"].ToString().Replace("Alpha", "");
        dash.text = keysConfig["DashButton"].ToString().Replace("Alpha", "");
        invetory.text = keysConfig["DisplayInvetoryButton"].ToString().Replace("Alpha", "");
    }

    void addToKeysConfig(string buttonName, string key)
    {
        if (PlayerPrefs.GetString(buttonName).Length > 0)
        {
            keysConfig.Add(buttonName, (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(buttonName, key)));
        }
        else
        {
            keysConfig.Add(buttonName, (KeyCode)System.Enum.Parse(typeof(KeyCode), key));
            PlayerPrefs.SetString(buttonName, key);
        }
    }
    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                if (!keysConfig.ContainsValue(e.keyCode))
                {
                    keysConfig[currentKey.name] = e.keyCode;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString().Replace("Alpha", "");
                }
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
            saveKeys();
        }
    }
    public void changeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }
        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }

    public void saveKeys()
    {
        foreach (var key in keysConfig)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }
}
