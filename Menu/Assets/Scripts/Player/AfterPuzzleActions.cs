using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterPuzzleActions : MonoBehaviour
{
    void Start()
    {
        if (Statics.winPuzzle)
        {
            GetComponent<PlayerUIUpdates>().updateExperience(Statics.expAfterPuzzle);
            GetComponent<PlayerUIUpdates>().SavePlayerDataToGlobal();
            Statics.winPuzzle = false;
        }
    }
}
