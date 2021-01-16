using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel1Trigger : MonoBehaviour
{
    public bool isBossAlive = true;
    void OnTriggerStay2D(Collider2D col)
    {
        if (!isBossAlive && col.CompareTag("Player"))
        {
           SceneManager.LoadScene("Credits");
        }
    }

}