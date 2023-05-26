using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VacayMode : MonoBehaviour
{
    private Image crossedOut;
    public Color selectedColor;
    public Color deselectedColor;
    public GameObject raid;

    void Start()
    {
        crossedOut = transform.GetChild(1).GetComponent<Image>();
        bool toggle = true;
        if (PlayerPrefs.GetInt("VacationMode") == 1)
        {
            toggle = false;
            crossedOut.color = deselectedColor;
        }
        else
        {
            crossedOut.color = selectedColor;
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
            crossedOut.color = deselectedColor;
        }
        else
        {
            PlayerPrefs.SetInt("VacationMode", 1);
            crossedOut.color = selectedColor;
        }
        raid.SetActive(toggle);
    }
}