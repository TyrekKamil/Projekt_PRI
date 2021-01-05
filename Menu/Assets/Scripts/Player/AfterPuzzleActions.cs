using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterPuzzleActions : MonoBehaviour
{
    void Start()
    {
        if (GLOBAL_DATA.Instance.winPuzzle)
        {
            GetComponent<PlayerUIUpdates>().updateExperience(GLOBAL_DATA.Instance.expAfterPuzzle);
            GetComponent<PlayerUIUpdates>().SavePlayerDataToGlobal();
            GLOBAL_DATA.Instance.winPuzzle = false;
        }
    }
}
