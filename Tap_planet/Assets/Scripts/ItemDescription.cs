using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    public GameObject emptyDescription;
    public GameObject item1Button;
    public GameObject item1;
    public GameObject item2Button;
    public GameObject item2;
    public GameObject item3Button;
    public GameObject item3;
    public GameObject item4Button;
    public GameObject item4;
    public GameObject item5Button;
    public GameObject item5;
    public GameObject item6Button;
    public GameObject item6;
    public GameObject item7Button;
    public GameObject item7;
    public GameObject item8Button;
    public GameObject item8;
    public GameObject item9Button;
    public GameObject item9;
    public GameObject item10Button;
    public GameObject item10;


    void Start()
    {
        gameObject.SetActive(true);
    }

    public void CloseTabsExcept(string tab) // different buttons send different arguments
    {
        emptyDescription.SetActive(CorrectTab(tab, "emptyDescription")); // Empty description
        item1.SetActive(CorrectTab(tab, "item1")); // only true if method was called with upgrade as argument
        item2.SetActive(CorrectTab(tab, "item2"));
        item3.SetActive(CorrectTab(tab, "item3"));
        item4.SetActive(CorrectTab(tab, "item4"));
        item5.SetActive(CorrectTab(tab, "item5"));
        item6.SetActive(CorrectTab(tab, "item6"));
        item7.SetActive(CorrectTab(tab, "item7"));
        item8.SetActive(CorrectTab(tab, "item8"));
        item9.SetActive(CorrectTab(tab, "item9"));
        item10.SetActive(CorrectTab(tab, "item10"));// false if called with upgrade
    }

    private bool CorrectTab(string tab, string thisTab)
    {
        if (tab.Equals(thisTab)) // checks if strings match
            return true;
        return false;
    }
}