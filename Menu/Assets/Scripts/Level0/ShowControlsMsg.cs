using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowControlsMsg : MonoBehaviour
{
    public GameObject uiObject;

    Text text;
    private string left;
    private string right;
    private string jump;
    private string action;


    // Start is called before the first frame update
    void Start()
    {
        left = PlayerPrefs.GetString("LeftButton").Replace("Arrow", "");
        right = PlayerPrefs.GetString("RightButton").Replace("Arrow", "");
        jump = PlayerPrefs.GetString("JumpButton");
        action = PlayerPrefs.GetString("ActionButton");        

        if (uiObject.name == "Ruch")
        {

            uiObject.SetActive(false);
            text = uiObject.GetComponent<Text>();
            text.text = "Press \"" + left + "\" or \"" + right + "\" to move!";
        }
        if (uiObject.name == "Skok")
        {
            uiObject.SetActive(false);
            text = uiObject.GetComponent<Text>();
            text.text = "Press \"" + jump + "\" to jump!";
        }

        if (uiObject.name == "SkokPodw")
        {
            uiObject.SetActive(false);
            text = uiObject.GetComponent<Text>();
            text.text = "Double tap \"" + jump + "\" to jump higher!";
        }

        if (uiObject.name == "Totem")
        {
            uiObject.SetActive(false);
            text = uiObject.GetComponent<Text>();
            text.text = "Focus on surrounding!";
        }

        if (uiObject.name == "Step")
        {
            uiObject.SetActive(false);
            text = uiObject.GetComponent<Text>();
            text.text = "Watch your steps!";
        }

        if (uiObject.name == "ColorsRiddle")
        {
            uiObject.SetActive(false);
            text = uiObject.GetComponent<Text>();
            text.text = "Let's see if you remember the right order of totem's lights";
        }
        if (uiObject.name == "PuzzleMinigame") 
        {
            uiObject.SetActive(false);
            text = uiObject.GetComponent<Text>();
            text.text = "Press \"" + action + "\" to start puzzle minigame";
        }
        if (uiObject.name == "RopesInfo") 
        {
            uiObject.SetActive(false);
            text = uiObject.GetComponent<Text>();
            text.text = "Press \"" + action + "\" to grab ropes. Get off the rope by jumping";
        }
        if (uiObject.name == "PipesInfo") 
        {
            uiObject.SetActive(false);
            text = uiObject.GetComponent<Text>();
            text.text = "Press \"" + action + "\" to move lever.";
        }
    }


    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            uiObject.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1);
        Destroy(uiObject);
        Destroy(gameObject);
    }

}
