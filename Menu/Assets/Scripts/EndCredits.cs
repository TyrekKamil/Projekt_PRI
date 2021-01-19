using UnityEngine.SceneManagement;
using UnityEngine;

public class EndCredits : MonoBehaviour
{
    public GameObject Credits;
    public GameObject Description;
    void Start()
    {
        Invoke("StartCredits", 5);
    }

    void StartCredits()
    {
        Description.SetActive(false);
        Credits.SetActive(true);
        Invoke("LoadSelectionLevel", 5);
    }

    void LoadSelectionLevel()
    {
        SceneManager.LoadScene("Level_Selection");

    }
}
