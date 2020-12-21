using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillsLoadingCircle : MonoBehaviour
{
    public Transform LoadingBar;
    public float cooldown = 1500f;
    [SerializeField] private float currentValue = 0f;
    void Update()
    {
        Debug.Log(currentValue);
        Debug.Log(currentValue/1500f);
        while (currentValue < cooldown)
        {
            LoadingBar.GetComponent<Image>().fillAmount = (currentValue / cooldown);
            currentValue += Time.deltaTime;
        }
        // Do zrobienia - cały czas daje wartość 1.
    }
}
    