using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void SceneLoader(int index) {
        SceneManager.LoadScene(index);
    }
}
