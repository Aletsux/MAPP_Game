using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPrice : MonoBehaviour
{
    public Button button;
    public Color activeColor;
    public Color inactiveColor;

    public string title;
    public int index;
    public int type;

    private StoreScript sc;
    public bool costsStardust;

    void Start()
    {
        sc = GameObject.FindGameObjectWithTag("Store").GetComponent<StoreScript>();
    }

    void Update()
    {
        if (costsStardust )
        {
            if (type == 1 && PlayerPrefs.GetInt("AccessoryPurchased_" + index) == 1)
            {
                button.image.color = activeColor;
            }
            if (type == 2 && PlayerPrefs.GetInt("PlanetPurchased_" + index) == 1)
            {
                button.image.color = activeColor;
            }
            else if (GameController.GetStardust() >= sc.GetPrice(title))
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
