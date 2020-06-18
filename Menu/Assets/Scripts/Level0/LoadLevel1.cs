using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadLevel1 : MonoBehaviour
{
    // LevelLoader loader;

    // [SerializeField] private Transform levelLoader;

    // void Start()
    // {
    //     loader = levelLoader.GetComponent<LevelLoader>();
    // }
    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.tag == "Player")
    //     {
    //         loader.LoadLevel(2);
    //     }
    // }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(2);

        }
    }


}
