using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowControlsMsg : MonoBehaviour
{
    public GameObject uiObject;
    Text text;

    string left, right;

    // Start is called before the first frame update
    void Start()
    {
        string left = PlayerPrefs.GetString("LeftButton").Replace("Arrow", "");
        string right = PlayerPrefs.GetString("RightButton").Replace("Arrow", "");
        uiObject.SetActive(false);
        text = uiObject.GetComponent<Text>();
        text.text = "Press \"" +  left + "\" or \"" + right + "\" to move" ;

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
        yield return new WaitForSeconds(5);
        Destroy(uiObject);
        // Destroy(gameObject);
    }

}
