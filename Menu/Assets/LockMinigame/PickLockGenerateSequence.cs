using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickLockGenerateSequence : MonoBehaviour
{
    public int numberOfSteps = 5;
    private int[] steps;
    void Start()
    {
        steps = new int[numberOfSteps];
        for (int i = 0; i < numberOfSteps; i++)
        {
            steps[i] = new System.Random().Next(1, 1000) % 2;
        }
    }

    void Update()
    {

    }
}
