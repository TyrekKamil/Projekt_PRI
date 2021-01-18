using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadLevel1 : MonoBehaviour
{
    public PlayerUIUpdates playerUI;
   
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            int level_selection_id = 11;
            GameObject loader = GameObject.Find("LevelLoader");
            loader.GetComponent<LevelLoader>().LoadLevel(level_selection_id);

        }
    }


}
