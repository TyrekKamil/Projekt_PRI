using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterPuzzleActions : MonoBehaviour
{
    void Start()
    {
        Debug.Log(Statics.winPuzzle);
        if (Statics.winPuzzle)
        {
            GetComponent<PlayerUIUpdates>().updateExperience(GLOBAL_DATA.Instance.expAfterPuzzle);
            GetComponent<PlayerUIUpdates>().SavePlayerDataToGlobal();
            Statics.winPuzzle = false;

        }
    }
}
