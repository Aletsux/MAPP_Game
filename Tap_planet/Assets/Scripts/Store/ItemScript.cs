using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ItemScript : MonoBehaviour
{
    public Sprite sprite;
    public String itemName;
    public String desciption;
    public String price;

    protected Text textComponent;
    private Button panelButton;
    private Button buyButton;
    public Color activeColor;
    public Color inactiveColor;

    public string title; // used to identify upgrades/powerups
    public int index; // used for accessories and planets
    public int type; // 0 for upgrades/powerups, 1 for accessories, 2 for planets

    protected StoreScript store;
    protected DescriptionScript desc;
    public bool costsStardust; // some upgrades/all accessories/all planets use stardust

    void Start()
    {
        store = GameObject.FindGameObjectWithTag("Store").GetComponent<StoreScript>();
        desc = GameObject.FindGameObjectWithTag("Description").GetComponent<DescriptionScript>();

        textComponent = gameObject.GetComponentsInChildren<Text>()[1];

        panelButton = gameObject.GetComponent<Button>();
        buyButton = gameObject.GetComponentsInChildren<Button>()[1];

        panelButton.onClick.AddListener(OnPanelClick);
        buyButton.onClick.AddListener(OnBuyClick);

        SetBuyButtonText();
    }

    void Update()
    {
        if (costsStardust)
        {
            if ((type == 1 && PlayerPrefs.GetInt("AccessoryPurchased_" + index) == 1) // true means owned
              || (type == 2 && PlayerPrefs.GetInt("PlanetPurchased_" + index) == 1)
              || (GameController.GetStardust() >= store.GetPrice(title)))
            {
                buyButton.image.color = activeColor;
            }
            else
            {
                buyButton.image.color = inactiveColor;
            }
        }
        else
        {
            if (GameController.GetCrystals() >= store.GetPrice(title))
            {
                buyButton.image.color = activeColor;
            }
            else
            {
                buyButton.image.color = inactiveColor;
            }
        }
    }

    public void OnPanelClick()
    {
        desc.GetAllInformation(this, false);
    }

    public virtual void OnBuyClick()
    {
        store.BuyPowerUp(title);
        SetBuyButtonText();
        desc.GetAllInformation(this, true);
    }

    public Sprite ReturnImage()
    {
        return sprite;
    }

    public String ReturnName()
    {
        return itemName;
    }

    public String ReturnDescription()
    {
        return desciption;
    }

    public virtual string ReturnPrice()
    {
        double price = store.GetPrice(title);
        if (price < 1000)
        {
            return price.ToString();
        }
        else if (price < 1000000)
        {
            return (price / 1000).ToString("F3") + "k";
        }
        else if (price < 1000000000)
        {
            return (price / 1000000).ToString("F3") + "M";
        }
        else
        {
            return (price / 1000000000).ToString("F3") + "B";
        }
    }

    protected virtual void SetBuyButtonText()
    {
        textComponent.text = ReturnPrice();
    }
}
