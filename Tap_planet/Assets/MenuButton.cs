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
    private bool selected;


    void Start()
    {
        selected = false;
        if (language == PlayerPrefs.GetInt("LanguageKey"))
            selected = true;
        SetColor();
    }

    public void SetColor()
    {
        if (selected)
            gameObject.GetComponent<Image>().color = selectedColor;
        else
            gameObject.GetComponent<Image>().color = deselectedColor;
        selected = !selected;
        if (otherButton.Selected() == this.selected)
            otherButton.DeselectMe();
    }

    private void DeselectMe()
    {
        SetColor();
    }
    public bool Selected()
    {
        return selected;
    }
}
