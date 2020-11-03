using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickLockGenerateSequence : MonoBehaviour
{
    public int numberOfMoves = 5;
    public int[] moves;
    private bool[] wasRandomed;
    void Start()
    {
        wasRandomed = new bool[100];
        for (int i = 0; i < 100; i++)
        {
            wasRandomed[i] = false;
        }
        moves = new int[numberOfMoves];
        for (int i = 0; i < numberOfMoves; i++)
        {
            moves[i] = randomSide();
        }
        GetComponent<PickLock>().enabled = true;
    }

    private int randomSide()
    {
        int random = 0;
        do
        {
            random = new System.Random().Next(1, 100);
        } while (wasRandomed[random - 1]);
        wasRandomed[random - 1] = true;
        return random % 2;
    }
}
