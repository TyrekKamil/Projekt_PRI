using UnityEngine.SceneManagement;
using UnityEngine;

public class EndCredits : MonoBehaviour
{
    void Start()
    {
        Invoke("WaitSec", 10);
    }

    void WaitSec()
    {
        SceneManager.LoadScene("Main Menu Scene");
    }
}
