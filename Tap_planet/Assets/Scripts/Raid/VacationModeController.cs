using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacationModeController : MonoBehaviour
{
    void Awake()
    {
        bool toggle = true;
        if (PlayerPrefs.GetInt("VacationMode") == 0)
        {
            toggle = false;
        }
        GameObject.FindAnyObjectByType<RaidController>().gameObject.SetActive(toggle);
        GameObject.FindAnyObjectByType<AndroidNotifications>().gameObject.SetActive(toggle);
    }

    public void ToggleVacationMode()
    {
        bool toggle = true;
        if (PlayerPrefs.GetInt("VacationMode") == 1)
        {
            toggle = false;
            PlayerPrefs.SetInt("VacationMode", 0);
        }
        else
        {
            PlayerPrefs.SetInt("VacationMode", 1);
        }
        GameObject.FindAnyObjectByType<RaidController>().gameObject.SetActive(toggle);
        GameObject.FindAnyObjectByType<AndroidNotifications>().gameObject.SetActive(toggle);
    }
}
