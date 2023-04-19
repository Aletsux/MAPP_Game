using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScript : MonoBehaviour
{
    public GameObject storeButton;
    [Space]
    public GameObject upgradeTabButton;
    public GameObject upgradeTab;
    [Space]
    public GameObject accessoryTabButton;
    public GameObject accessoryTab;
    [Space]
    public GameObject powerupTabButton;
    public GameObject powerupTab;
    [Space]
    //public GameObject planetTabButton;
    public GameObject planetTab;


    void Start()
    {
        CloseStore();
    }

    public void OpenStore()
    {
        gameObject.SetActive(true);
    }

    public void CloseStore()
    {
        gameObject.SetActive(false);
    }

    public void CloseTabsExcept(string tab) // different buttons send different arguments
    {
        upgradeTab.SetActive(CorrectTab(tab, "upgrade")); // only true if method was called with upgrade as argument
        accessoryTab.SetActive(CorrectTab(tab, "accessory")); // false if called with upgrade
        planetTab.SetActive(CorrectTab(tab, "planet"));
        powerupTab.SetActive(CorrectTab(tab, "powerup"));
    }

    private bool CorrectTab(string tab, string thisTab)
    {
        if (tab.Equals(thisTab)) // checks if strings match
            return true;
        return false;
    }
}