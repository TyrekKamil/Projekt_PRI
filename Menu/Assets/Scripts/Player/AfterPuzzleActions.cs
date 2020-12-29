using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterPuzzleActions : MonoBehaviour
{
    void Start()
    {
        Debug.Log(GLOBAL_DATA.Instance.winPuzzle);
        Debug.Log(GLOBAL_DATA.Instance.expAfterPuzzle);
        if (GLOBAL_DATA.Instance.winPuzzle)
        {
            GetComponent<PlayerUIUpdates>().updateExperience(GLOBAL_DATA.Instance.expAfterPuzzle);
            GetComponent<PlayerUIUpdates>().SavePlayerDataToGlobal();
            GLOBAL_DATA.Instance.winPuzzle = false;
        }
    }
}
