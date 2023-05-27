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
    protected string descriptionPrice = "Price";

    protected int galaxyLvl = 1; //Same As ActivePlanetIndex, update value after store items have been updated (updates at TogglePlanet)

    public virtual void Awake()
    {
        titleKey = "Title " + (index + 1);
        descriptionKey = "Desc " + (index + 1);
        
        store = GameObject.FindGameObjectWithTag("Store").GetComponent<StoreScript>();
        desc = GameObject.FindGameObjectWithTag("Description").GetComponent<DescriptionScript>();

        buyButtonText = transform.GetChild(2).GetComponentInChildren<Text>();

        panelButton = gameObject.GetComponent<Button>();
        buyButton = gameObject.GetComponentsInChildren<Button>()[1];

        panelButton.onClick.AddListener(OnPanelClick);
        buyButton.onClick.AddListener(OnBuyClick);
        
        defaultColor = gameObject.GetComponent<Image>().color;
    }
    public virtual void Start()
    {
        SetDescriptionTranslations();
        SetBuyButtonText();
        ToggleItemActive();
    }

    protected virtual void Update() //Toggle items based on current planet
    {
        if (galaxyLvl != PlayerPrefs.GetInt("ActivePlanetIndex")) {
            galaxyLvl = PlayerPrefs.GetInt("ActivePlanetIndex");
            Debug.Log("galaxyLvl: " + galaxyLvl);
        } 
        ToggleItemActive();

        if (ActiveCondition())
        {
            buyButton.image.color = activeColor;
        }
        else
        {
            buyButton.image.color = inactiveColor;
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
            return (price / 1000).ToString("F1") + "k";
        }
        else if (price < 1000000000)
        {
            return (price / 1000000).ToString("F1") + "M";
        }
        else if (price < 1000000000000)
        {
            return (price / 1000000000).ToString("F1") + "B";
        }
        else if (price < 1000000000000000)
        {
            return (price / 1000000000000).ToString("F1") + "T";
        }
        else if (price < 1000000000000000000)
        {
            return (price / 1000000000000000).ToString("F1") + "Q";
        }
        else
        {
            return (price / 1000000000000000000).ToString("F1") + "P";
        }
    }

    protected virtual void SetBuyButtonText()
    {
        buyButtonText.text = ReturnPrice();
    }

    public virtual void SetDescriptionTranslations()
    {
        itemName = LocalizationSettings.StringDatabase.GetLocalizedString(table, titleKey);
        description = LocalizationSettings.StringDatabase.GetLocalizedString(table, descriptionKey);

        if ((type == 1 && PlayerPrefs.GetInt("AccessoryPurchased_" + index) == 1) || (type == 2 && PlayerPrefs.GetInt("PlanetPurchased_" + index) == 1))
        {
            price = LocalizationSettings.StringDatabase.GetLocalizedString(table, descriptionPrice);
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
            //First planet -> 3 items active, for each new planet + 1 new item
            if (galaxyLvl == 0 && this.index <= scope)
            { //exception for first planet
                gameObject.GetComponent<Button>().image.color = defaultColor;
                buyButton.interactable = true;
            }
            else if (CheckItemActive(scope))
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
        int difference = this.index - galaxyLvl;
        //Debug.Log("Galaxy lvl: " + galaxyLvl + " " + this.itemName + " Differece: " + difference);
        return difference <= scope;
    }

    public virtual bool ActiveCondition()
    {
        long inBank = (costsStardust) ? GameController.GetStardust()  : GameController.GetCrystals();
        if (inBank >= store.GetPrice(title))
            return true;
        return false;
    }
}