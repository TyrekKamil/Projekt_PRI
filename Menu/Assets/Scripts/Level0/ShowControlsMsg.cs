using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowControlsMsg : MonoBehaviour
{
    public GameObject uiObject;

    Text text;


    // Start is called before the first frame update
    void Start()
    {
        if (uiObject.name == "Ruch")
        {
            string left = PlayerPrefs.GetString("LeftButton").Replace("Arrow", "");
            string right = PlayerPrefs.GetString("RightButton").Replace("Arrow", "");
            uiObject.SetActive(false);
            text = uiObject.GetComponent<Text>();
            text.text = "Press \"" + left + "\" or \"" + right + "\" to move!";
        }
        if (uiObject.name == "Skok")
        {
            string jump = PlayerPrefs.GetString("JumpButton");
            uiObject.SetActive(false);
            text = uiObject.GetComponent<Text>();
            text.text = "Press \"" + jump + "\" to jump!";
        }

        if (uiObject.name == "SkokPodw")
        {
            string jump = PlayerPrefs.GetString("JumpButton");
            uiObject.SetActive(false);
            text = uiObject.GetComponent<Text>();
            text.text = "Double tap \"" + jump + "\" to jump higher!";
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
        yield return new WaitForSeconds(3);
        Destroy(uiObject);
        Destroy(gameObject);
    }

}
