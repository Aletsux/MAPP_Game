using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
    public GameObject storeButton; // to open
    [Space]
    public GameObject upgradeTab; //different tabs
    public GameObject accessoryTab;
    public GameObject powerupTab;
    public GameObject planetTab;
    [Space]
    public GameObject GC; // gameController
    private GameController gameController; //actually gameController script!

    [Space]
    public Text UIText;
    [Space]
    //[SerializeField] int tpuCost = 1; //tpu = timedPowerUp
    [Space]
    private float timer; //counts up until it reaches timeToDisplay
    private float timeToDisplay = 2; // time to display message
    private bool displayUIMessage = false;

    //Accessoar
    public List<GameObject> accessoryObjects = new List<GameObject>();
    public List<Button> accessoryButtons;
    public List<int> accessoryCosts = new List<int>();

    //Planeter
    public List<GameObject> planetObjects = new List<GameObject>();
    public List<Button> planetButtons;
    public List<int> planetCosts = new List<int>();

    private string currentLanguage;

    void Start()
    {
        UIText.gameObject.SetActive(false);
        CloseStore();
        gameController = GC.GetComponent<GameController>(); // gets access to methods

        //PlayerPrefs.DeleteAll(); //Till för testning av accessoarer/planeter - ta bort om köp ska minnas efter omstart av spel, eller om det finns andra PlayerPrefs du inte vill ska påverkas
        //Accessoarer:
        for (int i = 0; i < accessoryObjects.Count; i++)
        {
            if (PlayerPrefs.GetInt("AccessoryEquipped_" + i) == 1)
            {
                accessoryObjects[i].SetActive(true);
                SetButtonLabel(accessoryButtons, i, "Unequip");
            }
            else if (PlayerPrefs.GetInt("AccessoryEquipped_" + i) == 0 && PlayerPrefs.GetInt("AccessoryPurchased_" + i) == 1)
            {
                accessoryObjects[i].SetActive(false);
                SetButtonLabel(accessoryButtons, i, "Equip");
                PlayerPrefs.SetInt("AccessoryEquipped_" + i, 0);
                PlayerPrefs.Save();
            }
            else if (PlayerPrefs.GetInt("AccessoryEquipped_" + i) == 0 && PlayerPrefs.GetInt("AccessoryPurchased_" + i) == 0)
            {
                accessoryObjects[i].SetActive(false);
                SetButtonLabel(accessoryButtons, i, accessoryCosts[i].ToString() + "SD");
                PlayerPrefs.SetInt("AccessoryEquipped_" + i, 0);
                PlayerPrefs.Save();
            }

            accessoryButtons[i].onClick.RemoveAllListeners();
            accessoryButtons[i].onClick.AddListener(() =>
            {
                EquipAccessory(i);
            });
        }

        //Planeter
        int activePlanetIndex = PlayerPrefs.GetInt("ActivePlanetIndex", 0);

        for (int i = 0; i < planetObjects.Count; i++)
        {
            if (i == activePlanetIndex) //Om i är den aktiva planeten
            {
                planetObjects[i].SetActive(true);
                SetButtonLabel(planetButtons, i, "Equipped");
                planetButtons[i].interactable = false;
            }
            else if (PlayerPrefs.GetInt("PlanetPurchased_" + i) == 1) //Om planeten har köpts tidigare
            {
                planetObjects[i].SetActive(false);
                SetButtonLabel(planetButtons, i, "");
                planetButtons[i].interactable = false;
            }
            else //Om planeten ej har köpts tidigare
            {
                planetObjects[i].SetActive(false);
                SetButtonLabel(planetButtons, i, planetCosts[i].ToString() + "SD");
                planetButtons[i].interactable = true;
            }

            planetButtons[i].onClick.RemoveAllListeners();
            planetButtons[i].onClick.AddListener(() => //lägger till listener för varje planet-knapp
            {
                EquipPlanet(i);
            });
        }

        //Om ingen planet är aktiverad, sätt startplaneten som aktiv
        if (activePlanetIndex == 0)
        {
            planetObjects[0].SetActive(true);
            SetButtonLabel(planetButtons, 0, "Equipped");
            planetButtons[0].interactable = false;
            PlayerPrefs.SetInt("PlanetPurchased_" + 0, 1);
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        if (displayUIMessage) // bool set to true in DisplayMessage()
        {
            timer += Time.deltaTime;
            if (timer >= timeToDisplay)
            {
                timer = 0f;
                UIText.gameObject.SetActive(false);
                displayUIMessage = false;
            }
        }

        LanguageSelector languageSelector = FindObjectOfType<LanguageSelector>();
        currentLanguage = languageSelector.GetCurrentLanguage();
        Debug.Log(currentLanguage);
    }

    public void OpenStore()
    {
        gameObject.SetActive(true);
    }

    public void CloseStore()
    {
        gameObject.SetActive(false);
        ResetTimer();
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

    public void BuyPowerUp(string powerUpName) // takes which powerup to buy
    {
        if (powerUpName.Equals("tpu")) // if tpu
        {
            if (GameController.GetCrystals() >= gameController.GetTpuCost()) // checks bank balance       
            {
                gameController.AddTPUAmount(); // adds 1 to tpuAmount
                GameController.DecreaseCrystals(gameController.GetTpuCost()); // reduces money in bank
                DisplayMessage("Timed boost purchased!"); // displays message
            }
            else
            {
                DisplayMessage("Not Enough Crystals!"); // if not enough crystals in bank
            }
        }
        else if (powerUpName.Equals("permanentClickPowerUp"))
        {
            if (GameController.GetCrystals() >= gameController.GetPermCost())
            {
                gameController.ClickIncrease();
                GameController.DecreaseCrystals(gameController.GetPermCost());
                DisplayMessage("Your clicks now give you " + GameController.ReturnClickIncrease() + " crystals!");
            }
            else
            {
                DisplayMessage("Not Enough Crystals!");
            }
        }
        else if (powerUpName.Equals("IdlePower")) {

            if(GameController.GetCrystals() >= gameController.GetIdleCost())
            {
                if (gameController.IsIdleTrue() == false)
                {
                    
                    DisplayMessage("You will now recieve " + gameController.ReturnClicksPerSec() + " crystal per minute!");
                }
                else if(gameController.IsIdleLvlTrue())
                {

                    DisplayMessage("You get " + gameController.ReturnClicksPerSec() + " crystals per second and " + gameController.ReturnClickPerTime() + " every " + (gameController.ReturnSecBeforeClick()) + " seconds");
                }


                gameController.BuyIdle();
                GameController.DecreaseCrystals(gameController.GetIdleCost());

            }
            else
            {
                DisplayMessage("Not Enough Crystals!");
            }
            
        }
        else if (powerUpName.Equals("stardustMiner"))
        {
            int cost = (GameController.GetStardustMinerLevel() == 0) ? 250 : GameController.GetStardustMinerLevel() * 100;
            if (GameController.GetStardustMinerLevel() == 20)
            {
                DisplayMessage("No more upgrades!");
            }
            else if (GameController.GetStardust() >= cost)
            {
                gameController.IncreaseStardustMinerLevel();
                GameController.DecreaseStardust(cost);
                DisplayMessage(GameController.GetStardustMinerLevel() + "% chance to find stardust!" );
            }
            else
            {
                DisplayMessage("Not Enough Stardust!");
            }
        }
        else
        {
            DisplayMessage("Error: No such item found!");
        }
    }

    public void DisplayMessage(string message) // called by actions the player makes such as buying upgrades or failing to buy upgrades
    {
        UIText.gameObject.SetActive(true); // important to activate and deactivate otherwise its always in the way
        UIText.text = message;
        displayUIMessage = true; // bool to tell timer to start
        ResetTimer(); //without this, the timer would run out and the message would dissappear even if player clicks multiple times after the initial
    }

    private void ResetTimer() 
    {
        timer = 0;
    }

    public void EquipAccessory(int index) //anropas vid klick av accessories-köpknapp
    {
        //if (index >= accessoryObjects.Count)
        //{
        //    Debug.LogError("Invalid index: " + index);
        //    return;
        //}

        bool hasPurchased = PlayerPrefs.GetInt("AccessoryPurchased_" + index, 0) == 1;

        if (!hasPurchased)
        {
            PurchaseAccessory(index);
        }
        else
        {
            ToggleAccessory(index);
        }
    }

    private void PurchaseAccessory(int index)
    {
        GameController.DecreaseStardust(accessoryCosts[index]);
        PlayerPrefs.SetInt("AccessoryPurchased_" + index, 1);
        PlayerPrefs.Save();
        SetButtonLabel(accessoryButtons, index, "Equip");
    }

    private void ToggleAccessory(int index) //ifall accessoaren är aktiverad inaktiveras den och vice versa
    {
        bool isEquipped = accessoryObjects[index].activeSelf; //om accessoar-gameobjectet är aktiverat

        //sätter för den klickade knappen
        if (isEquipped)
        {
            accessoryObjects[index].SetActive(false);
            SetButtonLabel(accessoryButtons, index, "Equip");
            PlayerPrefs.SetInt("AccessoryEquipped_" + index, 0);
            PlayerPrefs.Save();
        }
        else
        {
            accessoryObjects[index].SetActive(true);
            SetButtonLabel(accessoryButtons, index, "Unequip");
            PlayerPrefs.SetInt("AccessoryEquipped_" + index, 1);
            PlayerPrefs.Save();
        }
        //sätter för de andra knapparna
        for (int i = 0; i < accessoryObjects.Count; i++)
        {
            if (i != index && PlayerPrefs.GetInt("AccessoryPurchased_" + i) == 1)
            {
                accessoryObjects[i].SetActive(false);
                SetButtonLabel(accessoryButtons, i, "Equip");
                PlayerPrefs.SetInt("AccessoryEquipped_" + i, 0);
                PlayerPrefs.Save();
            }
            else if (i != index && PlayerPrefs.GetInt("AccessoryPurchased_" + i) == 0)
            {
                accessoryObjects[i].SetActive(false);
                SetButtonLabel(accessoryButtons, i, accessoryCosts[i].ToString() + "SD");
                PlayerPrefs.SetInt("AccessoryEquipped_" + i, 0);
                PlayerPrefs.Save();
            }
        }
    }

    private void SetButtonLabel(List<Button> buttons, int index, string label)
    {
        buttons[index].GetComponentInChildren<Text>().text = label;
    }

    public void EquipPlanet(int index)
    {
        if (PlayerPrefs.GetInt("PlanetPurchased_" + index) == 0)
        {
            purchasePlanet(index);
        }

        togglePlanet(index);
    }

    private void purchasePlanet(int index)
    {
        GameController.DecreaseStardust(planetCosts[index]);
        PlayerPrefs.SetInt("PlanetPurchased_" + index, 1);
        PlayerPrefs.Save();
    }

    private void togglePlanet(int index)
    {
        SetButtonLabel(planetButtons, index, "Equipped");
        planetButtons[index].interactable = false;
        planetObjects[index].SetActive(true);
        PlayerPrefs.SetInt("ActivePlanetIndex", index); //ifall man hämtar inten får man indexet för planeten som är equipped
        PlayerPrefs.Save();

        for (int i = 0; i < planetObjects.Count; i++)
        {
            if (i != index && PlayerPrefs.GetInt("PlanetPurchased_" + i) == 1) //ifall man tidigare haft planeten
            {
                planetObjects[i].SetActive(false);
                SetButtonLabel(planetButtons, i, "");
                planetButtons[i].interactable = false;
            }
            else if (i != index && PlayerPrefs.GetInt("PlanetPurchased_" + i) == 0)
            {
                planetObjects[i].SetActive(false);
                SetButtonLabel(planetButtons, i, planetCosts[i].ToString() + "SD");
                planetButtons[i].interactable = true;
            }
        }
    }

    private Dictionary<string, Dictionary<string, string>> getTranslations()
    {
        //En "tabell" med översättningar
        Dictionary<string, Dictionary<string, string>> translations = new Dictionary<string, Dictionary<string, string>>();
        translations["en"] = new Dictionary<string, string>() {
            { "Buy", "Buy" },
            { "Equip", "Equip" },
            { "Unequip", "Unequip" },
            { "Equipped", "Equipped" },
            { "", "" },
        };
        translations["sv"] = new Dictionary<string, string>() {
            { "Buy", "Köp" },
            { "Equip", "Sätt på" },
            { "Unequip", "Ta av" },
            { "Equipped", "På" },
            { "", "" },
        };

        return translations; //wtf am i doing
    }
}