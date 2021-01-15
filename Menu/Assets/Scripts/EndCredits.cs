using UnityEngine.SceneManagement;
using UnityEngine;

public class EndCredits : MonoBehaviour
{
    void Start()
    {
        Invoke("WaitSec", 6);
    }

    void WaitSec()
    {
        SceneManager.LoadScene("Level_Selection");
    }
}
