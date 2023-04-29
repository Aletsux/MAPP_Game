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
        gameController = GC.GetComponent<GameController>(); // gets access to methods
        PlayerPrefs.DeleteAll(); //Till för testning av accessoarer/planeter - ta bort om köp ska minnas efter omstart av spel, eller om det finns andra PlayerPrefs du inte vill ska påverkas 
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
                SetButtonLabel(accessoryButtons, i, "Buy"); 
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
                SetButtonLabel(planetButtons, i, "Equipped"); 
                planetButtons[i].interactable = false; 
            } 
            else if (PlayerPrefs.GetInt("PlanetPurchased_" + i) == 1) //Om planeten har köpts tidigare 
            { 
                planetObjects[i].SetActive(false); 
                SetButtonLabel(planetButtons, i, "Completed"); 
                planetButtons[i].interactable = false; 
            } 
            else //Om planeten ej har köpts tidigare 
            { 
                planetObjects[i].SetActive(false); 
                SetButtonLabel(planetButtons, i, "Buy"); 
                planetButtons[i].interactable = true; 
            } 
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

    public void EquipAccessory(int index) //anropas vid klick av accessories-köpknapp
    {
        //if (index >= accessoryObjects.Count)
        //{
        //    Debug.LogError("Invalid index: " + index);
        //    return;
        //}

        //Debug.Log(index);

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
            SetButtonLabel(accessoryButtons, index, "Equip");
        }
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
                SetButtonLabel(accessoryButtons, i, "Buy");
                PlayerPrefs.SetInt("AccessoryEquipped_" + i, 0);
                PlayerPrefs.Save();
            }
        }
    }

    private void SetButtonLabel(List<Button> buttons, int index, string textObjectTag)
    {
        GameObject buttonObject = buttons[index].gameObject;
        foreach (Text textObject in buttonObject.GetComponentsInChildren<Text>())
        {
            if (textObject.CompareTag(textObjectTag))
            {
                textObject.enabled = true;
            }
            else
            {
                textObject.enabled = false;
            }
        }
    }

    public void EquipPlanet(int index)
    {
        if (GameController.GetStardust() >= planetCosts[index])
        {
            purchasePlanet(index);
            togglePlanet(index);
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
        SetButtonLabel(planetButtons, index, "Equipped");
        planetButtons[index].interactable = false;
        planetObjects[index].SetActive(true);
        PlayerPrefs.SetInt("ActivePlanetIndex", index); //ifall man h�mtar inten f�r man indexet f�r planeten som �r equipped
        PlayerPrefs.Save();

        for (int i = 0; i < planetObjects.Count; i++)
        {
            if (i != index && PlayerPrefs.GetInt("PlanetPurchased_" + i) == 1) //ifall man tidigare haft planeten
            {
                planetObjects[i].SetActive(false);
                SetButtonLabel(planetButtons, i, "Completed");
                planetButtons[i].interactable = false;
            }
            else if (i != index && PlayerPrefs.GetInt("PlanetPurchased_" + i) == 0)
            {
                planetObjects[i].SetActive(false);
                SetButtonLabel(planetButtons, i, "Buy");
                planetButtons[i].interactable = true;
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
                gameController.ClickIncrease();
                GameController.DecreaseCrystals(GetPrice(powerUpName));
            }
        }
        else if (powerUpName.Equals("idle"))
        {
            if (GameController.GetCrystals() >= GetPrice(powerUpName))
            {
                gameController.BuyIdle();
                GameController.DecreaseCrystals(GetPrice(powerUpName));
            }
        }
        else if (powerUpName.Equals("dust"))
        {
            int cost = (GameController.GetStardustMinerLevel() == 0) ? 20 : GameController.GetStardustMinerLevel() * 50;
            if (GameController.GetStardust() >= cost)
            {
                gameController.IncreaseStardustMinerLevel();
                GameController.DecreaseStardust(cost);
            }
        }

        gameController.SaveGame();
    }

    public int GetPrice(string name)
    {
        if (name.Equals("idle"))
        {
            return gameController.GetIdleCost();
        }
        else if (name.Equals("perm"))
        {
            return gameController.GetPermCost() * (12);
        }
        else if (name.Equals("temp"))
        {
            return gameController.GetTpuCost();
        }
        else if (name.Equals("dust"))
        {
            return (GameController.GetStardustMinerLevel() == 0) ? 50 : GameController.GetStardustMinerLevel() * 100;
        }

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
        return 0;
    }
}