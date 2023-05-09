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
    public Button buyButton;
    protected Text buyButtonText;
    public Color activeColor; 
    public Color inactiveColor;
    private Color defaultColor;

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

    protected int galaxyLvl = 1; //Same As ActivePlanetIndex, update value after store items have been updated (ToggleItemActive)

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

        defaultColor = gameObject.GetComponent<Image>().color;
        ToggleItemActive();
    }

    void Update()
    {
        if (galaxyLvl != PlayerPrefs.GetInt("ActivePlanetIndex", 0)) {
            ToggleItemActive();
            galaxyLvl = PlayerPrefs.GetInt("ActivePlanetIndex", 0);
        }
        
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

        if ((type == 1 && PlayerPrefs.GetInt("AccessoryPurchased_" + index) == 1) || (type == 2 && PlayerPrefs.GetInt("PlanetPurchased_" + index) == 1))
        {
            price = LocalizationSettings.StringDatabase.GetLocalizedString(table, priceKey);
        }
        else
        {
            SetBuyButtonText();
        }
    }

    //Set inactive color of button panel if item index is outside of scope 
    //if active, set color to defaultColor and set interactable
    public void ToggleItemActive() {
        int scope = 2;
        if(!costsStardust) {
            if (CheckItemActive(scope))
            {
                gameObject.GetComponent<Button>().image.color = defaultColor;
                buyButton.interactable = true;
            }
            else
            {
                gameObject.GetComponent<Button>().image.color = inactiveColor;
                buyButton.interactable = false;
            }
        }
    }

    //Check If item is outside of scope
    private bool CheckItemActive(int scope) {
        int difference = this.index - PlayerPrefs.GetInt("ActivePlanetIndex", 0) + 1;
        Debug.Log(this.itemName + " Differece: " + difference);
        return difference <= scope;
    }
}