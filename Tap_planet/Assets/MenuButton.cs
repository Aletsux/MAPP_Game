using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public int language;
    public MenuButton otherButton;
    public Color selectedColor;
    public Color deselectedColor;
    private bool selected = false;

    void Start()
    {
        if (language == PlayerPrefs.GetInt("LanguageKey"))
            SetActiveColor();
    }

    public void SetActiveColor()
    {
        gameObject.GetComponent<Image>().color = selectedColor;
        selected = true;
        otherButton.SetInactiveColor();
    }

    private void SetInactiveColor()
    {
        gameObject.GetComponent<Image>().color = deselectedColor;
        selected = false;
    }
}