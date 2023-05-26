using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class VacayMode : MonoBehaviour
{
    private Text buttonText;
    private string table = "UI Text";
    private string key;
    public GameObject raid;

    void Start()
    {
        buttonText = transform.GetChild(0).GetComponent<Text>();
        bool toggle = true;
        if (PlayerPrefs.GetInt("VacationMode") == 1)
        {
            toggle = false;
            key = "raidoff";
        }
        else
        {
            key = "raidon";
        }
        raid.SetActive(toggle);
        UpdateText();
    }

    public void ToggleVacationMode()
    {
        bool toggle = true;
        if (PlayerPrefs.GetInt("VacationMode") == 1)
        {
            toggle = false;
            PlayerPrefs.SetInt("VacationMode", 0);
            key = "raidoff";
        }
        else
        {
            PlayerPrefs.SetInt("VacationMode", 1);
            key = "raidon";
        }
        UpdateText();
        raid.SetActive(toggle);
    }

    public void UpdateText()
    {
        buttonText.text = LocalizationSettings.StringDatabase.GetLocalizedString(table, key);
    }
}
