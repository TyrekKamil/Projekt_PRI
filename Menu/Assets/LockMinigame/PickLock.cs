﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickLock : MonoBehaviour
{
    private Vector3 currentEulerAngles;
    private Quaternion currentRotation;
    public Transform picklockTransform;
    private int[] moves;
    private int direction = 0;
    private int step = 0;
    public GameObject correctText;
    public GameObject badText;
    public GameObject succcesText;
    public GameObject helpText;
    private GameObject lastText;
    private bool win = false;

    void Start()
    {
        if (Statics.itemDropped)
        {
            Statics.itemDropped = false;
        }
        lastText = helpText;
        string text = helpText.GetComponent<Text>().text;
        text = text.Replace("LeftButton", PlayerPrefs.GetString("LeftButton").Replace("Arrow", ""));
        text = text.Replace("RightButton", PlayerPrefs.GetString("RightButton").Replace("Arrow", ""));
        helpText.GetComponent<Text>().text = text;
        moves = GetComponent<PickLockGenerateSequence>().moves;
        Debug.Log(moves[0] + ", " + moves[1] + ", " + moves[2] + ", " + moves[3] + ", " + moves[4]);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && direction == 0)
        {
            direction = -1;
            lastText.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.D) && direction == 0)
        {
            direction = 1;
            lastText.SetActive(false);
        }
        if (direction == 1 || direction == -1)
        {
            move();
        }
        if (direction == 2 || direction == -2)
        {
            moveCenter();
        }
        if (step == GetComponent<PickLockGenerateSequence>().numberOfMoves)
        {
            StartCoroutine("WaitForSec");
            win = true;
            exit();
        }
    }

    void move()
    {
        picklockTransform.position += new Vector3(10 * direction, 0, 0) * 0.1f;
        if (Mathf.Abs(picklockTransform.localPosition.x) > 20)
        {
            checkMove(direction);
            direction = direction == 1 ? -2 : 2;
        }
    }
    void moveCenter()
    {
        picklockTransform.position += new Vector3(5 * direction, 0, 0) * 0.1f;
        if ((direction / 2) * picklockTransform.localPosition.x > 0)
        {
            direction = 0;
        }
    }

    void checkMove(int checkMoveDirection)
    {
        checkMoveDirection = checkMoveDirection == 1 ? 1 : 0;
        if (moves[step] == checkMoveDirection)
        {
            step++;
            lastText = correctText;
        }
        else
        {
            step = 0;
            lastText = badText;
        }
        if (step == GetComponent<PickLockGenerateSequence>().numberOfMoves)
        {
            lastText = succcesText;
            Statics.chestOpened = true;
        }
        lastText.SetActive(true);
    }

    public void exit()
    {
        if (win)
        {
            GLOBAL_DATA.Instance.chestOpened[GLOBAL_DATA.Instance.actualChestIndex] = true;
            GLOBAL_DATA.Instance.actualChestIndex = 0;
        }
        SceneManager.LoadScene(Statics.lastSceneId);
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
    }
}
