using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSave : MonoBehaviour
{
    private GameObject saverInput;


    private void Start()
    {
        saverInput = GameObject.Find("Game Saved info");
        saverInput.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SaveLoad.SeriouslyDeleteAllSaveFiles();
            GameEvents.OnSaveSceneInitiated();
            GameEvents.OnSaveInitiated();
            saverInput.SetActive(true);
            StartCoroutine("WaitForSec");

            if (gameObject.name == "Lights")
            {
                Transform[] allChildren = GetComponentsInChildren<Transform>(true);
                foreach (Transform child in allChildren)
                {
                    child.gameObject.SetActive(true);
                }

            }    
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        saverInput.SetActive(false);
    }

   
}
