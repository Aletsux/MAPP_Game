using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VacayMode : MonoBehaviour
{
    private Image button;
    public Color selectedColor;
    public Color deselectedColor;
    public GameObject raid;

    void Start()
    {
        button = GetComponent<Image>();
        bool toggle = true;
        button.color = selectedColor;
        if (PlayerPrefs.GetInt("VacationMode") == 0)
        {
            toggle = false;
            button.color = deselectedColor;
        }
        raid.SetActive(toggle);
    }

    public void ToggleVacationMode()
    {
        bool toggle = true;
        if (PlayerPrefs.GetInt("VacationMode") == 1)
        {
            toggle = false;
            PlayerPrefs.SetInt("VacationMode", 0);
            button.color = deselectedColor;
        }
        else
        {
            PlayerPrefs.SetInt("VacationMode", 1);
            button.color = selectedColor;
        }
        raid.SetActive(toggle);
    }
}