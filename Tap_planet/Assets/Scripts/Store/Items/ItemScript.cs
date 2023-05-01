using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;

public class ItemScript : MonoBehaviour
{
    public Sprite itemSprite;
    public String itemName;
    public String description;
    public String price;

    private Button panelButton;
    private Button buyButton;
    protected Text buyButtonText;
    public Color activeColor; 
    public Color inactiveColor;

    public string title; // used to identify upgrades/powerups
    public int index; // used for accessories and planets
    public int type; // 0 for upgrades/powerups, 1 for accessories, 2 for planets

    protected StoreScript store;
    protected DescriptionScript desc;
    public bool costsStardust; // some upgrades/all accessories/all planets use stardust

    protected string table;
    protected string titleKey = "Title ";
    protected string descriptionKey = "Desc ";
    protected string priceKey = "Price";

    public virtual void Start()
    {
        titleKey = "Title " + (index + 1);
        descriptionKey = "Desc " + (index + 1);
        
        store = GameObject.FindGameObjectWithTag("Store").GetComponent<StoreScript>();
        desc = GameObject.FindGameObjectWithTag("Description").GetComponent<DescriptionScript>();

        buyButtonText = gameObject.GetComponentsInChildren<Text>()[1];

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
            if ((type == 1 && PlayerPrefs.GetInt("AccessoryPurchased_" + index) == 1)  || (type == 2 && PlayerPrefs.GetInt("PlanetPurchased_" + index) == 1)  || (GameController.GetStardust() >= store.GetPrice(title)))
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

    protected virtual void OnPanelClick()
    {
        desc.GetAllInformation(this, false);
    }

    protected virtual void OnBuyClick()
    {
        print("itemscript");
        store.BuyPowerUp(title);
        SetBuyButtonText();
        desc.GetAllInformation(this, true);
    }

    public Sprite ReturnImage()
    {
        return itemSprite;
    }

    public String ReturnName()
    {
        return itemName;
    }

    public String ReturnDescription()
    {
        return description;
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
        buyButtonText.text = ReturnPrice();
    }

    public void GetStringForUI()
    {
        itemName = LocalizationSettings.StringDatabase.GetLocalizedString(table, titleKey);
        description = LocalizationSettings.StringDatabase.GetLocalizedString(table, descriptionKey);
        price = LocalizationSettings.StringDatabase.GetLocalizedString(table, priceKey);
    }
}
