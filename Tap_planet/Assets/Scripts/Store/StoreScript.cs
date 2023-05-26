using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    public GameObject upgradeTabButton; //different tab-buttons
    public GameObject accessoryTabButton;
    public GameObject powerupTabButton;
    public GameObject planetTabButton;
    [Space]
    public GameObject GC; // gameController
    private GameController gameController; //actually gameController script!

    //Accessoar
    public List<GameObject> accessoryObjects = new List<GameObject>();
    public List<Button> accessoryButtons;
    public List<int> accessoryCosts = new List<int>();

    //Planeter
    public List<GameObject> planetObjects = new List<GameObject>();
    public List<Button> planetButtons;
    public List<int> planetCosts = new List<int>();

    void Awake()
    {
        gameObject.SetActive(true);
        gameController = GC.GetComponent<GameController>();// gets access to methods
        //PlayerPrefs.DeleteAll(); //Till för testning av accessoarer/planeter - ta bort om köp ska minnas efter omstart av spel, eller om det finns andra PlayerPrefs du inte vill ska påverkas 
        //Accessoarer: 
        for (int i = 0; i < accessoryObjects.Count; i++)
        {
            if (PlayerPrefs.GetInt("AccessoryEquipped_" + i) == 1)
            {
                accessoryObjects[i].SetActive(true);
                //SetButtonLabel(accessoryButtons, i, "Unequip");
            }
            else if (PlayerPrefs.GetInt("AccessoryEquipped_" + i) == 0 && PlayerPrefs.GetInt("AccessoryPurchased_" + i) == 1)
            {
                accessoryObjects[i].SetActive(false);
                //SetButtonLabel(accessoryButtons, i, "Equip");
                PlayerPrefs.SetInt("AccessoryEquipped_" + i, 0);
                PlayerPrefs.Save();
            }
            else if (PlayerPrefs.GetInt("AccessoryEquipped_" + i) == 0 && PlayerPrefs.GetInt("AccessoryPurchased_" + i) == 0)
            {
                accessoryObjects[i].SetActive(false);
                //SetButtonLabel(accessoryButtons, i, "Buy");
                PlayerPrefs.SetInt("AccessoryEquipped_" + i, 0);
                PlayerPrefs.Save();
            }
        }

        //Planeter 
        int activePlanetIndex = PlayerPrefs.GetInt("ActivePlanetIndex", 0);

        for (int i = 0; i < planetObjects.Count; i++)
        {
            if (i == activePlanetIndex) //Om i är den aktiva planeten 
            {
                planetObjects[i].SetActive(true);
                //SetButtonLabel(planetButtons, i, "Equipped");
                planetButtons[i].interactable = false;
            }
            else if (PlayerPrefs.GetInt("PlanetPurchased_" + i) == 1) //Om planeten har köpts tidigare 
            {
                planetObjects[i].SetActive(false);
                //SetButtonLabel(planetButtons, i, "Completed");
                planetButtons[i].interactable = false;
            }
            else //Om planeten ej har köpts tidigare 
            {
                planetObjects[i].SetActive(false);
                //SetButtonLabel(planetButtons, i, "Buy");
                planetButtons[i].interactable = true;

                if (i != 0)
                {
                    if (PlayerPrefs.GetInt("PlanetPurchased_" + (i - 1)) == 0)
                    {
                        planetButtons[i].interactable = false;
                    }
                    else
                    {
                        planetButtons[i].interactable = true;
                    }
                }
            }
        }

        //Om ingen planet är aktiverad, sätt startplaneten som aktiv 
        if (activePlanetIndex == 0)
        {
            planetObjects[0].SetActive(true);
            //SetButtonLabel(planetButtons, 0, "Equipped");
            planetButtons[0].interactable = false;
            PlayerPrefs.SetInt("PlanetPurchased_" + 0, 1);
            PlayerPrefs.Save();
        }
    }
    void Start()
    {
        StartCoroutine(InvokeMethodAfterStart());
    }

    private IEnumerator InvokeMethodAfterStart()
    {
        yield return null;
        CloseStore();
    }

    public void OpenStore()
    {
        gameObject.SetActive(true);
        CloseTabsExcept("upgrade");
    }

    public void CloseStore()
    {
        gameObject.SetActive(false);
    }

    public void CloseTabsExcept(string tab) // different buttons send different arguments
    {
        upgradeTabButton.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
        accessoryTabButton.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
        planetTabButton.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
        powerupTabButton.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);

        
        upgradeTab.SetActive(CorrectTab(tab, "upgrade")); // only true if method was called with upgrade as argument
        accessoryTab.SetActive(CorrectTab(tab, "accessory")); // false if called with upgrade
        planetTab.SetActive(CorrectTab(tab, "planet"));
        powerupTab.SetActive(CorrectTab(tab, "powerup"));

        if (CorrectTab(tab, "upgrade"))
        {
            UpgradeScript[] levels = GameObject.FindObjectsByType<UpgradeScript>(FindObjectsSortMode.None);
            foreach (UpgradeScript level in levels)
            {
                level.SetLevelText();
            }
        }
        if (CorrectTab(tab, "powerup"))
        {
            PowerupScript[] amounts = GameObject.FindObjectsByType<PowerupScript>(FindObjectsSortMode.None);
            foreach (PowerupScript amount in amounts)
            {
                if (amount.isUsingAmount)
                    amount.SetAmountText();
            }
        }
        if (upgradeTab.activeSelf)
        {
            upgradeTabButton.GetComponent<Image>().color = Color.white;
        }
        if (accessoryTab.activeSelf)
        {
            accessoryTabButton.GetComponent<Image>().color = Color.white;
        }
        if (planetTab.activeSelf)
        {
            planetTabButton.GetComponent<Image>().color = Color.white;
        }
        if (powerupTab.activeSelf)
        {
            powerupTabButton.GetComponent<Image>().color = Color.white;
        }
    }

    private bool CorrectTab(string tab, string thisTab)
    {
        if (tab.Equals(thisTab)) // checks if strings match
            return true;
        return false;
    }

    public void EquipAccessory(int index) //anropas vid klick av accessories-köpknapp
    {
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
        print("index: " + index);
        print("accessoryCosts[index]: " + accessoryCosts[index]);
        print(GameController.GetStardust() >= accessoryCosts[index]);
        if (GameController.GetStardust() >= accessoryCosts[index])
        {
            GameController.DecreaseStardust(accessoryCosts[index]);
            PlayerPrefs.SetInt("AccessoryPurchased_" + index, 1);
            PlayerPrefs.Save();
            //SetButtonLabel(accessoryButtons, index, "Equip");
        }
    }

    private void ToggleAccessory(int index) //ifall accessoaren är aktiverad inaktiveras den och vice versa
    {
        bool isEquipped = accessoryObjects[index].activeSelf; //om accessoar-gameobjectet är aktiverat

        //sätter för den klickade knappen
        if (isEquipped)
        {
            accessoryObjects[index].SetActive(false);
            //SetButtonLabel(accessoryButtons, index, "Equip");
            PlayerPrefs.SetInt("AccessoryEquipped_" + index, 0);
            PlayerPrefs.Save();
        }
        else
        {
            accessoryObjects[index].SetActive(true);
            //SetButtonLabel(accessoryButtons, index, "Unequip");
            PlayerPrefs.SetInt("AccessoryEquipped_" + index, 1);
            PlayerPrefs.Save();
        }
        //sätter för de andra knapparna - detta var för ifall bara en accessoar kunde vara aktiverad
        //for (int i = 0; i < accessoryObjects.Count; i++)
        //{
        //    if (i != index && PlayerPrefs.GetInt("AccessoryPurchased_" + i) == 1)
        //    {
        //        accessoryObjects[i].SetActive(false);
        //        SetButtonLabel(accessoryButtons, i, "Equip");
        //        PlayerPrefs.SetInt("AccessoryEquipped_" + i, 0);
        //        PlayerPrefs.Save();
        //    }
        //    else if (i != index && PlayerPrefs.GetInt("AccessoryPurchased_" + i) == 0)
        //    {
        //        accessoryObjects[i].SetActive(false);
        //        SetButtonLabel(accessoryButtons, i, "Buy");
        //        PlayerPrefs.SetInt("AccessoryEquipped_" + i, 0);
        //        PlayerPrefs.Save();
        //    }
        //}
    }

    //private void SetButtonLabel(List<Button> buttons, int index, string textObjectTag)
    //{
    //    GameObject buttonObject = buttons[index].gameObject;
    //    foreach (Text textObject in buttonObject.GetComponentsInChildren<Text>())
    //    {
    //        if (textObject.CompareTag(textObjectTag))
    //        {
    //            textObject.enabled = true;
    //        }
    //        else
    //        {
    //            textObject.enabled = false;
    //        }
    //    }
    //}

    public void EquipPlanet(int index)
    {
        int previousPlanet = index - 1;
        if (GameController.GetStardust() >= planetCosts[index] && PlayerPrefs.GetInt("PlanetPurchased_" + previousPlanet) == 1)
        {
            purchasePlanet(index);
            togglePlanet(index);
            planetButtons[previousPlanet].GetComponentInParent<PlanetScript>().SetCompleted();
        }
    }

    private void purchasePlanet(int index)
    {
        GameController.DecreaseStardust(planetCosts[index]);
        PlayerPrefs.SetInt("PlanetPurchased_" + index, 1);
        PlayerPrefs.Save();
    }

    private void togglePlanet(int index)
    {
        //SetButtonLabel(planetButtons, index, "Equipped");
        planetButtons[index].interactable = false;
        planetObjects[index].SetActive(true);
        PlayerPrefs.SetInt("ActivePlanetIndex", index); //ifall man h�mtar inten f�r man indexet f�r planeten som �r equipped
        PlayerPrefs.Save();

        for (int i = 0; i < planetObjects.Count; i++)
        {
            if (i != index && PlayerPrefs.GetInt("PlanetPurchased_" + i) == 1) //ifall man tidigare haft planeten
            {
                planetObjects[i].SetActive(false);
                //SetButtonLabel(planetButtons, i, "Completed");
                planetButtons[i].interactable = false;
            }
            else if (i != index && PlayerPrefs.GetInt("PlanetPurchased_" + i) == 0)
            {
                planetObjects[i].SetActive(false);
                //SetButtonLabel(planetButtons, i, "Buy");
                planetButtons[i].interactable = true;

                if (i != 0)
                {
                    if (PlayerPrefs.GetInt("PlanetPurchased_" + (i - 1)) == 0)
                    {
                        planetButtons[i].interactable = false;
                    }
                    else
                    {
                        planetButtons[i].interactable = true;
                    }
                }
            }
        }
    }

    public void BuyPowerUp(string powerUpName) // takes which powerup to buy
    {
        if (powerUpName.Equals("temp")) // if tpu
        {
            if (GameController.GetCrystals() >= GetPrice(powerUpName)) // checks bank balance       
            {
                gameController.AddTPUAmount(); // adds 1 to tpuAmount
                GameController.DecreaseCrystals(GetPrice(powerUpName)); // reduces money in bank
            }
        }
        else if (powerUpName.Equals("perm"))
        {
            if (GameController.GetCrystals() >= GetPrice(powerUpName))
            {
                gameController.ClickLevelUp();
                GameController.DecreaseCrystals(GetPrice(powerUpName));
            }
        }
        else if (powerUpName.Equals("idle"))
        {
            if (GameController.GetCrystals() >= GetPrice(powerUpName))
            {
                gameController.BuyIdle();
                GameController.DecreaseCrystals(GetPrice(powerUpName));
                GameObject.FindGameObjectWithTag("Player").GetComponent<MrTwinky>().ActivateTwinky();
            }
        }
        else if (powerUpName.Equals("dust"))
        {
            int cost = GetPrice(powerUpName);
            if (GameController.GetCrystals() >= cost)
            {
                gameController.IncreaseStardustMinerLevel();
                GameController.DecreaseCrystals(cost);
            }
        }
        else if (powerUpName.Equals("star"))
        {
            if (GameController.IsIdleTrue() && GameController.GetCrystals() >= GetPrice(powerUpName))
            {
                int i = PlayerPrefs.GetInt("IdleExtenderLvl");
                PlayerPrefs.SetInt("IdleExtenderLvl", i + 1);
                GameController.DecreaseCrystals(GetPrice(powerUpName));
            }
        }
        else if (powerUpName.Equals("raidWipe"))
        {
            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {
                int i = PlayerPrefs.GetInt("WipeEnemiesAmount");
                PlayerPrefs.SetInt("WipeEnemiesAmount", i + 1);

                double higherCost = PlayerPrefs.GetInt("RaidWipeCost") * 1.05;
                PlayerPrefs.SetInt("RaidWipeCost", (int)Math.Ceiling(higherCost));

                GameController.DecreaseStardust(GetPrice(powerUpName));

                print(PlayerPrefs.GetInt("WipeEnemiesAmount"));
                print(PlayerPrefs.GetInt("RaidWipeCost"));
            }
        }
        else if (powerUpName.Equals("doubletime"))
        {
            if (GameController.IsIdleTrue() && GameController.GetCrystals() >= GetPrice(powerUpName))
            {
                //PlayerPrefs.SetInt("DoubleTime", PlayerPrefs.GetInt("DoubleTime") + 1);
                GameController.DecreaseCrystals(GetPrice(powerUpName));
                DoubleTime.IncreaseCost();
            }
        }

        else if (powerUpName.Equals("shield"))
        {
            int shieldLevel = PlayerPrefs.GetInt("ShieldLevel");
            int shieldCost = PlayerPrefs.GetInt("ShieldCost");

            if (GameController.GetStardust() >= GetPrice(powerUpName))
            {

                if (shieldLevel <= 0)
                {
                    PlayerPrefs.SetInt("ShieldLevel", 1);
                    PlayerPrefs.SetInt("ShieldCost", 10000);
                }
                else
                {
                    PlayerPrefs.SetInt("ShieldCost", (int)(shieldCost * 1.2));
                    PlayerPrefs.SetInt("ShieldLevel", shieldLevel + 1);
                }
                Debug.Log("PRE: " + PlayerPrefs.GetInt("healthBoostAmount"));
                int i = PlayerPrefs.GetInt("healthBoostAmount");
                PlayerPrefs.SetInt("healthBoostAmount", i + 1);
                Debug.Log("HBA: " + PlayerPrefs.GetInt("healthBoostAmount"));
                GameController.DecreaseStardust(GetPrice(powerUpName));
                gameController.SaveGame();
            }
            else
            {
                //Knapp inaktiverad.
            }
        }
    }
    //Set updates time for all items in store
    public int GetPrice(string name)
    {
        //Powerups / upgrades
        if (name.Equals("idle"))
        {
            return gameController.GetIdleCost();
        }
        else if (name.Equals("perm"))
        {
            return GameController.GetClickLvl() * (5);
        }
        else if (name.Equals("temp"))
        {
            return gameController.GetTpuCost();
        }
        else if (name.Equals("dust"))
        {
            return (GameController.GetStardustMinerLevel() == 0) ? 1000 : (GameController.GetStardustMinerLevel() * 10000 * (130 / 100)); //increase 30%
        }
        else if (name.Equals("star"))
        {
            return (PlayerPrefs.GetInt("IdleExtenderLvl") == 0) ? 1000 : PlayerPrefs.GetInt("IdleExtenderLvl") * 10000;
        }
        else if (name.Equals("doubletime"))
        {
            return (int) DoubleTime.GetCost();
        }
        else if (name.Equals("raidWipe"))
        {
            if (PlayerPrefs.GetInt("RaidWipeCost") == 0)
            {
                PlayerPrefs.SetInt("RaidWipeCost", 10);
            }
            return PlayerPrefs.GetInt("RaidWipeCost");
        }
        else if (name.Equals("shield"))
        {
            return gameController.GetShieldCost();
        }

        //Accessories
        else if (name.Equals("party"))
        {
            return accessoryCosts[1];
        }
        else if (name.Equals("cow"))
        {
            return accessoryCosts[2];
        }
        else if (name.Equals("halo"))
        {
            return accessoryCosts[3];
        }
        else if (name.Equals("cap"))
        {
            return accessoryCosts[4];
        }
        else if (name.Equals("glasses"))
        {
            return accessoryCosts[5];
        }
        else if (name.Equals("orangeTie"))
        {
            return accessoryCosts[6];
        }
        else if (name.Equals("purpleTie"))
        {
            return accessoryCosts[7];
        }
        else if (name.Equals("purpleTie"))
        {
            return accessoryCosts[7];
        }
        else if (name.Equals("lights"))
        {
            return accessoryCosts[8];
        }
        else if (name.Equals("leaf"))
        {
            return accessoryCosts[9];
        }

        //Planets
        else if (name.Equals("drip"))
        {
            return planetCosts[1];
        }
        else if (name.Equals("cookie"))
        {
            return planetCosts[2];
        }
        else if (name.Equals("candy"))
        {
            return planetCosts[3];
        }
        else if (name.Equals("melon"))
        {
            return planetCosts[4];
        }
        else if (name.Equals("tomato"))
        {
            return planetCosts[5];
        }
        else if (name.Equals("swirl"))
        {
            return planetCosts[6];
        }
        return 0;
    }



    public List<int> GetAccessoryCost()
    {
        return accessoryCosts;
    }
    public List<int> GetPlanetCost()
    {
        return planetCosts;
    }
}