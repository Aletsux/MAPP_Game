using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPrice : MonoBehaviour
{
    public Button button;
    public Color activeColor;
    public Color inactiveColor;

    public string title; // used to identify upgrades/powerups
    public int index; // used for accessories and planets
    public int type; // 0 for upgrades/powerups, 1 for accessories, 2 for planets

    private StoreScript sc;
    public bool costsStardust; // some upgrades/all accessories/all planets use stardust

    void Start()
    {
        sc = GameObject.FindGameObjectWithTag("Store").GetComponent<StoreScript>();
    }

    void Update()
    {
        if (costsStardust)
        {
            if ( (type == 1 && PlayerPrefs.GetInt("AccessoryPurchased_" + index) == 1) // true means owned
              || (type == 2 && PlayerPrefs.GetInt("PlanetPurchased_" + index) == 1)
              || (GameController.GetStardust() >= sc.GetPrice(title)) )
            {
                button.image.color = activeColor;
            }
            else
            {
                button.image.color = inactiveColor;
            }
        }
        else
        {
            if (GameController.GetCrystals() >= sc.GetPrice(title))
            {
                button.image.color = activeColor;
            }
            else
            {
                button.image.color = inactiveColor;
            }
        }
    }
}